using System.ComponentModel;
using NahimicSettingsControlLib;

namespace NahimicSwitcher
{
    public class NahimicProfile
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal readonly INSProfile _nsProfile;

        public NahimicProfile(INSProfile nsProfile)
        {
            _nsProfile = nsProfile;
        }

        public string Name
        {
            get
            {
                string name;
                _nsProfile.GetName(out name);
                return name;
            }
        }
    }
}