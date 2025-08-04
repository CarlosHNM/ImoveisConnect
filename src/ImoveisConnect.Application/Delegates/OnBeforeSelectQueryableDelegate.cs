namespace ImoveisConnect.Application.Delegates
{
    public delegate void OnBeforeSelectQueryableDelegate<T>(ref IQueryable<T> query);
}
