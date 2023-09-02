using Assessment.Context;
using Assessment.DtoModels;
using Assessment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assessment.DataAccess
{
    public class PlatformWellDal
    {
        private readonly DataContext _dataContext;
        
        public PlatformWellDal (DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void DbExecuteNonResult (List<PlatformDto> platforms)
        {
            try
            {
                if (platforms != null)
                {
                    foreach (var data in platforms)
                    {
                        var platformData = _dataContext.Platforms.Include(x=>x.Wells).Where(x=>x.Id == data.Id).FirstOrDefault();

                        if (platformData != null)
                        {
                            platformData.UniqeuName = data.UniqueName;
                            platformData.Latitude = data.Latitude;
                            platformData.Longitude = data.Longitude;
                            platformData.CreatedAt = data.CreatedAt;
                            platformData.UpdatedAt = data.UpdatedAt;

                            _dataContext.Platforms.Update(platformData);

                            foreach (var wellDta in platformData.Wells.Where(x=>x.PlatformId == data.Id))
                            {
                                var wellData = platformData.Wells.Where(x=>x.Id == wellDta.Id).FirstOrDefault();
                                if (wellData != null)
                                {
                                    wellData.UniqueName = wellDta.UniqueName;
                                    wellData.Latitude = wellDta.Latitude;
                                    wellData.Longitude = wellDta.Longitude;
                                    wellData.CreatedAt = wellDta.CreatedAt;
                                    wellData.UpdatedAt = wellDta.UpdatedAt;

                                    _dataContext.Wells.Update(wellData);
                                }
                            }
                        }

                        //create data
                        else
                        {
                            var platform = new Platform();

                            platform.Id = data.Id;
                            platform.UniqeuName = data.UniqueName;
                            platform.Latitude = data.Latitude;
                            platform.Longitude = data.Longitude;
                            platform.CreatedAt = data.CreatedAt;
                            platform.UpdatedAt = data.UpdatedAt;
                            platform.Wells = data.Well.Select(x => new Well
                            {
                                Id = x.Id,
                                PlatformId = x.PlatformId,
                                UniqueName = x.UniqueName,
                                Latitude = x.Latitude,
                                Longitude = x.Longitude,
                                CreatedAt = x.CreatedAt,
                                UpdatedAt = x.UpdatedAt,
                            }).ToList();
                            _dataContext.Platforms.Add(platform);
                        }
                    }
                    _dataContext.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
