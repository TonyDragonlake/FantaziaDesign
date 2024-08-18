namespace FantaziaDesign.Core
{
	public interface IVector<T>
	{
		T StartPointX { get; set; }
		T StartPointY { get; set; }
		T EndPointX { get; set; }
		T EndPointY { get; set; }

		void GetVectorRaw(out T spx, out T spy, out T epx, out T epy);
		void GetStartPointRaw(out T spx, out T spy);
		void GetEndPointRaw(out T epx, out T epy);
		void SetVector(T spx, T spy, T epx, T epy);
		void SetStartPoint(T spx, T spy);
		void SetEndPoint(T epx, T epy);
		void Offset(T offsetX, T offsetY);
		void ZeroStartPoint();
		void ZeroEndPoint();
		void ZeroVector();
	}

}
