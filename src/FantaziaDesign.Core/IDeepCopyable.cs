using System;
namespace FantaziaDesign.Core
{
	public interface IDeepCopyable<T> : ICloneable
	{
		T DeepCopy();
		void DeepCopyValueFrom(T obj);
	}
}

