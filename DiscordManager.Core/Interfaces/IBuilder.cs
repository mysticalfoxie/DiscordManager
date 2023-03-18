namespace DCM.Core.Interfaces;

internal interface IBuilder<out TOut> where TOut : class
{
    public TOut Build();
}