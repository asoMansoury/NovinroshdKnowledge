using DataMatrixPrinter.Common.CommonUtils;
using DataMatrixPrinter.DomainClasses.Entities.Identites;
using DataMatrixPrinter.ViewModels.Business.UserVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataMatrixPrinter.DomainClasses.Entities.Business;

namespace DataMatrixPrinter.AutoMapper.ManualMapping
{
    public class UserMapping
    {
        #region MapVM
        public static UserVM Map(ApplicationUser model)
        {
            var map = new UserVM()
            {
                password = model.PasswordHash,
                email = model.Email,
                name = model.Name,
                nationalCode = model.UserName,
                Id = model.Id,
                family = model.Family,
                imageName = model.ImageName,
                isEnable = model.isEnable            
            };
            return map;
        }

        public static List<UserVM> Map(List<ApplicationUser> users)
        {
            var result = new List<UserVM>();
            result = users.ConvertAll(mapData);
            return result;
        }

        private static UserVM mapData(ApplicationUser input)
        {
            return new UserVM
            {
                imageName = input.ImageName,
                email = input.Email,
                family = input.Family,
                name = input.Name,
                Id = input.Id,
                nationalCode = input.UserName,
                isEnable = input.isEnable,
            };
        }
        #endregion

        #region MapModel
        public static ApplicationUser Map(UserVM model)
        {

            
            return new ApplicationUser()
            {
                ImageName = model.imageName,
                UserName = model.nationalCode,
                Email = model.email,
                Name=model.name,
                Family = model.family,
                PasswordHash =  model.password,
                isEnable = model.isEnable
            };
        }

        public static List<CustomRole> MapRoles(UserVM map)
        {
            var data = !string.IsNullOrEmpty(map.roleName )? map.roleName.Split(',').ToList():null;

            var result = new List<CustomRole>();
            if(data!=null)
            data.ForEach(e =>
            {
                result.Add(new CustomRole { Id = int.Parse(e) });
            });
            return result;
        }
        #endregion
    }
}
