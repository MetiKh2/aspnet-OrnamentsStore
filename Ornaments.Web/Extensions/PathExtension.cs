using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioMeti.Application.Utilities.Utils
{
    public static class PathExtension
    {

        #region Ornament image
        public static string OrnamentImageOriginPath = "/Content/Ornament/Image/origin/";
        public static string OrnamentImageOriginSever = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Ornament/Image/origin/");

        public static string OrnamentImageThumbPath = "/Content/Ornament/Image/Thumb/";
        public static string OrnamentImageThumbSever = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Ornament/Image/Thumb/");
        #endregion
       
    }
}
