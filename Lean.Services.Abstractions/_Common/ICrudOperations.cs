namespace Lean.Services.Abstractions._Common;

public interface ICrudOperations<in TAdd, in TEdit, in TSearchModel, TOutSearch> : IAddable<TAdd>, IEditable<TEdit>,
    IDeletable, ISearchable<TSearchModel, TOutSearch>
{
}