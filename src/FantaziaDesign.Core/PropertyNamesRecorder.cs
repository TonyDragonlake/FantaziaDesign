using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantaziaDesign.Core
{
	public sealed class PropertyNamesRecorder<TClass> : IPropertyNamesRecorder
	{
		private static readonly string[] s_propNames = typeof(TClass).GetProperties().Select(p => p.Name).ToArray();

		private readonly bool[] m_recorder = new bool[s_propNames.Length];

		private PropertyNamesRecorder()
		{
		}

		public static IPropertyNamesRecorder Create()
		{
			return new PropertyNamesRecorder<TClass>();
		}

		public IEnumerable<string> OfChanged()
		{
			var total = s_propNames.Length;
			for (int i = 0; i < total; i++)
			{
				if (m_recorder[i])
				{
					yield return s_propNames[i];
				}
			}
		}

		public bool TrySetRecorder(string propName, bool changed)
		{
			if (string.IsNullOrWhiteSpace(propName))
			{
				return false;
			}
			int index = Array.IndexOf(s_propNames, propName);
			return TrySetRecorder(index, changed);
		}

		public bool TrySetRecorder(int index, bool changed)
		{
			if (index < 0 || index >= s_propNames.Length)
			{
				return false;
			}
			m_recorder[index] = changed;
			return true;
		}

		public void ClearRecorder()
		{
			Array.Clear(m_recorder);
		}
	}
}

