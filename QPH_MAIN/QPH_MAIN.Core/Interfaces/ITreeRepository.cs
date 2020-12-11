using QPH_MAIN.Core.Entities;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ITreeRepository
    {
        Task<Tree> GetTreeByUserId(string userName, string aplication, string enterprise);
    }
}