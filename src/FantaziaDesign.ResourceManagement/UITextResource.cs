using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FantaziaDesign.ResourceManagement
{
	public class UITextResource : IResourceNotifier<string, string>, IDisposable
	{
		private class __Factory : IResourceNotifierFactory<string, string>
		{
			private static readonly Dictionary<string, UITextResource> s_txtResDict = new Dictionary<string, UITextResource>();

			public string FactoryName => nameof(UITextResource);

			public IResourceNotifier<string, string> CreateFromKey(string key)
			{
				if (!s_txtResDict.TryGetValue(key, out var res))
				{
					res = new UITextResource(key);
					s_txtResDict.Add(key, res);
				}
				return res;
			}
		}

		public static readonly IResourceNotifierFactory<string, string> Factory = new __Factory();

		private bool m_isDisposed;
		private string m_key;

		public event PropertyChangedEventHandler PropertyChanged;

		private UITextResource(string key)
		{
			m_key = key;
			LanguagePackageManager.Current.ResourceChanged += OnResourceChanged;
		}

		private void OnResourceChanged(object sender, IResourceContainer<string, string> args)
		{
			if (string.Equals(args.Key, Key))
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Resource)));
			}
		}

		public string Resource => LanguagePackageManager.Current.ProvideResource(Key, string.Empty);

		public string Key => m_key;

		public void SetResource(string resource) { }

		public void Dispose()
		{
			if (m_isDisposed)
			{
				return;
			}
			LanguagePackageManager.Current.ResourceChanged -= OnResourceChanged;
			m_isDisposed = true;
		}
	}

}
