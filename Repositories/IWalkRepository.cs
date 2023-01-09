using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalkRepository
    {
        List<Walk> GetAllWalks();
        Walk GetWalkById(int id);
        List<Walk> GetWalkByWalkerId(int walkerId);
        string WalkTimeByWalker(int id);
    }
}
