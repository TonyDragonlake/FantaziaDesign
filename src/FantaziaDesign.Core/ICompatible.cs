using System;

namespace FantaziaDesign.Core
{
	public interface ICompatible<T> : IEquatable<T>, IComparable<T>
	{
		void AsCompatible(out T value);
	}
}
