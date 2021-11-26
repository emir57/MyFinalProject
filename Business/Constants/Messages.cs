using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string ProductsListed="Ürünler listelendi";
        public static string MaintenanceTime= "Sistem Bakımda";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir";

        public static string ProductCheckForName = "Aynı isimde ürün olamaz";

        public static string CategoryLimitExceed = "Kategori Sayısı 15'i geçtiği için ürün eklenemiyor";

        public static string AuthorizationDenied = "Erişim Engellendi";
    }
}
