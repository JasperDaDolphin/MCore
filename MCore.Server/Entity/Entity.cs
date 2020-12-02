using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Server.Entity
{
	public abstract class Entity
	{
        /// <summary>
        /// Loads players into memory
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Force saves players
        /// </summary>
        public abstract void ForceSave();

        /// <summary>
        /// Force saves players async
        /// </summary>
        public abstract Task ForceSaveAsync();
    }
}
