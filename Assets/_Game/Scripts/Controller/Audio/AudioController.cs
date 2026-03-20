
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioItem
{
    public string key;

    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;

    public bool loop;
}


public sealed class AudioController : EventTarget
{

    public static class AudioKeys
    {
        public const string BgmMainMenu = "BGM_MainMenu";
        public const string BgmCombatBoss = "BGM_Combat_Boss";
        public const string BgmEventArea1 = "BGM_EventArea_1";
        public const string BgmEventArea2 = "BGM_EventArea_2";
        public const string BgmEventArea3 = "BGM_EventArea_3";
        public const string BgmEventArea4 = "BGM_EventArea_4";
        public const string BgmMap1 = "BGM_Map_1";
        public const string BgmMap2 = "BGM_Map_2";
        public const string BgmMap3 = "BGM_Map_3";

        public const string UiClick = "UI_Click";
        public const string UiHover = "UI_Hover";
        public const string UiChestOpen = "UI_Chest_Open";
        public const string UiItemPickup = "UI_Item_Pickup";
        public const string UiItemDropEquip = "UI_Item_Drop_Equip";
        public const string UiItemMergeUpgrade = "UI_Item_Merge/Upgrade";

        public const string SfxAttack = "SFX_Attack";
        public const string SfxHit = "SFX_Hit";
        public const string SfxEnemyDie = "SFX_Enemy_Die";
        public const string SfxPlayerDie = "SFX_Player_Die";
    }

    public static AudioController Instance { get; private set; }

    [Header("BGM clips")]
    [SerializeField] private List<AudioItem> bgmItems = new();

    [Header("SFX clips")]
    [SerializeField] private List<AudioItem> sfxItems = new();

    private Dictionary<string, AudioSource> _audioItemsDict = new();
    private HashSet<string> _bgmKeys = new();

    private string _currentBgmKey;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitAudioItems(bgmItems, true);
        InitAudioItems(sfxItems, false);
    }

    private void InitAudioItems(List<AudioItem> items, bool isBgm)
    {
        foreach (var item in items)
        {
            if (string.IsNullOrWhiteSpace(item.key) || item.clip == null)
            {
                continue;
            }

            if (_audioItemsDict.ContainsKey(item.key))
            {
                Debug.LogWarning($"Audio key duplicated: {item.key}");
                continue;
            }

            var sourceName = isBgm ? $"bgm_{item.key}" : $"sfx_{item.key}";
            var audioSource = Instantiate(new GameObject(sourceName), transform).AddComponent<AudioSource>();
            audioSource.clip = item.clip;
            audioSource.volume = item.volume;
            audioSource.loop = item.loop;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f;

            _audioItemsDict[item.key] = audioSource;

            if (isBgm)
            {
                _bgmKeys.Add(item.key);
            }
        }
    }

    void Start()
    {
        // PlayBgm(AudioKeys.BgmMainMenu);
    }

    public void Play(string key, bool loop = false)
    {
        if (_audioItemsDict.TryGetValue(key, out AudioSource audioSource))
        {
            audioSource.loop = loop;
            audioSource.Play();
        }
    }

    public void PlaySfx(string key)
    {
        if (_audioItemsDict.TryGetValue(key, out AudioSource audioSource))
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    public void PlayBgm(string key, bool restartIfSame = false)
    {
        if (!_bgmKeys.Contains(key))
        {
            Debug.LogWarning($"BGM key not found: {key}");
            return;
        }

        if (!restartIfSame && _currentBgmKey == key)
        {
            return;
        }

        StopCurrentBgm();

        if (_audioItemsDict.TryGetValue(key, out AudioSource audioSource))
        {
            _currentBgmKey = key;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopCurrentBgm()
    {
        if (string.IsNullOrEmpty(_currentBgmKey))
        {
            return;
        }

        if (_audioItemsDict.TryGetValue(_currentBgmKey, out AudioSource audioSource))
        {
            audioSource.Stop();
        }

        _currentBgmKey = null;
    }

    public void Stop(string key)
    {
        if (_audioItemsDict.TryGetValue(key, out AudioSource audioSource))
        {
            audioSource.Stop();
        }
    }
}