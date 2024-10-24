using WebApi.Interfaces.Mappers;

public class CollectionMapper<TSource, TDestination> : ICollectionMapper<TSource, TDestination>
{
    public ICollection<TDestination> Map(ICollection<TSource> sources)
    {
        if (sources is null || !sources.Any())
        {
            return new List<TDestination>();
        }

        var mapped = new List<TDestination>();

        foreach (var source in sources)
        {
            mapped.Add(itemMapper.Map(source));
        }

        return mapped;
    }

    public void Map(ICollection<TSource> sources, ICollection<TDestination> destinations)
    {
        if (sources is null)
        {
            throw new ArgumentNullException(nameof(sources));
        }

        if (destinations is null)
        {
            throw new ArgumentNullException(nameof(destinations));
        }

        if (!shouldAdd && !shouldRemove && sources.Count() != destinations.Count())
        {
            throw new InvalidOperationException("Mapping exception: the number of elements in the source and destination collections don't match.");
        }


        var sourceEnumerator = sources.GetEnumerator();
        var destinationEnumerator = destinations.GetEnumerator();
        var sourceHasValue = sourceEnumerator.MoveNext();
        var destinationHasValue = destinationEnumerator.MoveNext();

        // Iterating over both collections since the smaller one hits its end.
        while (sourceHasValue &&  destinationHasValue)
        {
            var source = sourceEnumerator.Current;
            var destination = destinationEnumerator.Current;

            itemMapper.Map(source, destination);
            sourceHasValue = sourceEnumerator.MoveNext();
            destinationHasValue = destinationEnumerator.MoveNext();
        }

        // adding items to destination, that are extra in source on top of already loaded in destionation
        if (shouldAdd && destinations.Count < sources.Count && sourceHasValue && !destinationHasValue)
        {
            do // if destination finishes before source, we will have source.Current already loaded, so we use post-condition here.
            {
                var mapped = itemMapper.Map(sourceEnumerator.Current);
                destinations.Add(mapped);
                sourceHasValue = sourceEnumerator.MoveNext();
            }
            while (sourceHasValue);
        }

        // removing items from destination, that are extra on top of already loaded in source
        if (shouldRemove && destinations.Count > sources.Count && !sourceHasValue && destinationHasValue)
        {
            var toRemove = new List<TDestination>();
            destinationHasValue = destinationEnumerator.MoveNext();

            while (destinationHasValue) // when source finishes first, MoveNext() of destination wouldn't be executed, because of &&, so we use pre-condition loop
            {
                toRemove.Add(destinationEnumerator.Current);
                destinationHasValue = destinationEnumerator.MoveNext();
            };

            // we loop in a separate cycle cached items for removal, since we cannot modify original collection while enumerator is being enumerating it.
            foreach (var candidate in toRemove)
            {
                destinations.Remove(candidate);
            }
        }

    }


    public CollectionMapper(IMapper<TSource, TDestination> itemMapper)
        : this(itemMapper, true, true)
    {
    }

    public CollectionMapper(IMapper<TSource, TDestination> itemMapper, bool shouldAdd, bool shouldRemove)
    {
        this.itemMapper = itemMapper ?? throw new ArgumentNullException(nameof(itemMapper));
        this.shouldAdd = shouldAdd;
        this.shouldRemove = shouldRemove;
    }


    private readonly IMapper<TSource, TDestination> itemMapper;
    private readonly bool shouldAdd = false;
    private readonly bool shouldRemove = false;
}