namespace ImoveisConnect.Application.Delegates
{
    public delegate void OnSpecificationOrderByEvaluating<TEntity>(ref IQueryable<TEntity> query);
}
