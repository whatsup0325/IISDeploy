using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISDeploy.BuildStrategy
{
    public class BuildManager
    {
        private readonly IBuildStrategy _buildStrategy;

        
        public BuildManager(IBuildStrategy buildStrategy)
        {
            _buildStrategy = buildStrategy;
        }

        public string ExecuteBuild(string sourcePath)
        {
            return _buildStrategy.Build(sourcePath);
        }
    }

    public interface IBuildStrategy
    {
        public event Action<string>? OutputStringChanged;
        string Build(string sourcePath);
    }
}
