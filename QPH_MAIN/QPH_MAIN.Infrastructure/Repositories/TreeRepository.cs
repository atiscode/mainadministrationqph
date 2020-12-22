using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class TreeRepository :  ITreeRepository
    {
        readonly QPHContext _context;
        public TreeRepository(QPHContext context) 
        {
            _context = context;
        }

        public async Task<Tree> GetTreeByUserId(string userName, string aplication, string enterprise)
        {
            try
            {
                var result = await _context.Tree.FromSqlRaw("exec GetHierarchyViewSchemaByUser @nickname={0}, @application={1}, @enterprise={2}", userName, aplication, enterprise).ToListAsync();

                if (!result.Any()) return new Tree();

                var parentRoot = await _context.Tree.FromSqlRaw("select top 1 id as parent, 'root' as title, '' as route, 1 as children, id as Id from [View] where code = 'root'").FirstOrDefaultAsync();
                result.Add(parentRoot);
                Dictionary<int, Tree> dict = result.ToDictionary(loc => loc.son, loc => new Tree { son = loc.son, route = loc.route, parent = loc.parent, title = loc.title, id = loc.son });

                foreach (Tree loc in dict.Values)
                {
                    var cards = await _context.Permissions.ToListAsync();

                    List<PermissionStatus> listCards = new List<PermissionStatus>();
                    List<PermissionStatus> listPermissions = new List<PermissionStatus>();
                    foreach (var card in cards)
                    {
                        if (card.is_card ?? false)
                            listCards.Add(new PermissionStatus
                            {
                                id = card.Id,
                                permission = card.permission,
                                status = card.status
                            });
                        else
                            listPermissions.Add(new PermissionStatus
                            {
                                id = card.Id,
                                permission = card.permission,
                                status = card.status
                            });
                    }

                    loc.cards = listCards;
                    loc.permissions = listPermissions;
                    if (loc.parent != loc.id)
                    {
                        Tree parent = dict[loc.parent];
                        parent.Children.Add(loc);
                    }
                }

                Tree root = dict.Values.First(loc => loc.parent == loc.id);
                return root;
            }
            catch (Exception)
            {
                return new Tree();
            }
        }
    }
}