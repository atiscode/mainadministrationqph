﻿using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Services
{
    public class TreeService : ITreeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TreeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Tree> GetHierarchyTreeByUserId(string userName, string aplication, string enterprise)
        {
            var tree = await _unitOfWork.TreeRepository.GetTreeByUserId(userName, aplication, enterprise);
            return tree;
        }
    }
}