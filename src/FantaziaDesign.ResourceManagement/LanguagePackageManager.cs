using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using FantaziaDesign.Core;
using FantaziaDesign.Events;

namespace FantaziaDesign.ResourceManagement
{
	public sealed class LanguagePackageManager : IResourceProvider<string, string>, INotifyResourceChanged<string, string>
	{
		public static readonly LanguagePackageManager Current = new LanguagePackageManager();

		private long m_uId;
		private LanguagePackage m_currentPkg;
		private Dictionary<string, LanguagePackage> m_pkgDict;
		private ObservableCollection<string> m_pkgNames;
		private WeakEvent<ResourceChangedEventHandler<string, string>> m_resourceChangedEvent;

		public event ResourceChangedEventHandler<string, string> ResourceChanged { add => AddHandler(value); remove => RemoveHandler(value); }

		private void RemoveHandler(ResourceChangedEventHandler<string, string> value)
		{
			m_resourceChangedEvent.RemoveHandler(value);
		}

		private void AddHandler(ResourceChangedEventHandler<string, string> value)
		{
			m_resourceChangedEvent.AddHandler(value);
		}

		public string ProviderName => nameof(LanguagePackageManager);

		public long UId => m_uId;

		public string CurrentPackageName => m_currentPkg?.LanguageKey ?? string.Empty;

		public int CurrentPackageIndex => m_pkgNames.IndexOf(CurrentPackageName);

		public IReadOnlyList<string> PackageNames { get => m_pkgNames; }

		private LanguagePackageManager()
		{
			m_uId = SnowflakeUId.Next();
			m_pkgDict = new Dictionary<string, LanguagePackage>();
			m_pkgNames = new ObservableCollection<string>();
			m_resourceChangedEvent = new WeakEvent<ResourceChangedEventHandler<string, string>>();
		}

		public LanguagePackage TryLoadLanguagePackage(Stream inputStream, IValueConverter<Stream, LanguagePackage> converter)
		{
			ArgumentNullException.ThrowIfNull(inputStream, nameof(inputStream));
			ArgumentNullException.ThrowIfNull(converter, nameof(converter));
			var pkg = converter.ConvertTo(inputStream);
			return TryAddPackage(pkg);
		}

		public LanguagePackage TryLoadLanguagePackage(string inputString, IValueConverter<string, LanguagePackage> converter)
		{
			if (string.IsNullOrWhiteSpace(inputString))
			{
				return null;
			}
			ArgumentNullException.ThrowIfNull(converter, nameof(converter));
			var pkg = converter.ConvertTo(inputString);
			return TryAddPackage(pkg);
		}

		private LanguagePackage TryAddPackage(LanguagePackage package)
		{
			if (package != null)
			{
				var key = package.LanguageKey;
				if (!m_pkgDict.ContainsKey(key))
				{
					m_pkgDict.Add(key, package);
					m_pkgNames.Add(key);
					return package;
				}
			}
			return null;
		}

		public bool TrySelectLanguage(string languageKey)
		{
			if (m_pkgDict.TryGetValue(languageKey, out var result))
			{
				m_currentPkg = result;
				foreach (var item in m_currentPkg)
				{
					RaiseResourceChangedEvent(new ResourceChangedEventArgs<string, string>(item.Key, item.Value));
				}
				return true;
			}
			return false;
		}

		public bool TrySelectLanguage(int lcid)
		{
			var culture = CultureInfo.GetCultureInfo(lcid);
			return TrySelectLanguage(culture.Name);
		}

		public string ProvideResource(string txtKey, string defaultText = null)
		{
			if (m_currentPkg is null)
			{
				if (string.IsNullOrWhiteSpace(defaultText))
				{
					return string.Empty;
				}
				return defaultText;
			}
			return m_currentPkg.ProvideResource(txtKey, defaultText);
		}

		public bool CombineWith(IResourceProvider<string, string> provider)
		{
			return false;
		}

		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return m_currentPkg?.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_currentPkg?.GetEnumerator();
		}

		private void RaiseResourceChangedEvent(IResourceContainer<string, string> eventArgs)
		{
			m_resourceChangedEvent.RaiseEvent(new WeakResourceChangedEventArgs<string, string>(this, eventArgs));
		}

	}


}
