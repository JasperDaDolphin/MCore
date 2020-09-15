using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Server.Storage {

    public class TestModel {

        [MaxLength(17)]
        public string SteamId { get; set; }

        [Key]
        public string TestString { get; set; }

    }
}
