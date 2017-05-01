using System.Collections.Generic;
using NahimicSettingsControlLib;

namespace NahimicSwitcher
{
    public class Nahimic
    {
        private readonly INSSimpleControl _nsSimpleControl;
        private readonly INSStore _nsStore;

        public Nahimic()
        {
            _nsSimpleControl = new NSSimpleControl();
            _nsSimpleControl.OpenStore(out _nsStore);
        }

        public IEnumerable<NahimicProfile> Profiles
        {
            get
            {
                int count;
                _nsSimpleControl.GetProfileCount(out count);
                for (int nIndex = 0; nIndex < count; ++nIndex)
                {
                    INSProfile profile;
                    _nsSimpleControl.GetProfile(nIndex, out profile);
                    yield return new NahimicProfile(profile);
                }
            }
        }

        public NahimicProfile CurrentProfile
        {
            get
            {
                INSProfile nsProfile;
                _nsSimpleControl.OpenCurrentProfile(out nsProfile);
                return new NahimicProfile(nsProfile);
            }
            set { _nsSimpleControl.SetCurrentProfile(value._nsProfile); }
        }

        public bool Enabled
        {
            get
            {
                INSSetting nsSetting;
                _nsStore.OpenSetting(ENahimicSetting.kNhSet_NahimicRenderState, out nsSetting);
                bool enabled;
                nsSetting.GetBoolSetting(out enabled);
                return enabled;
            }
            set
            {
                INSSetting nsSetting;
                _nsStore.OpenSetting(ENahimicSetting.kNhSet_NahimicRenderState, out nsSetting);
                nsSetting.SetBoolSetting(value);
            }
        }
    }
}