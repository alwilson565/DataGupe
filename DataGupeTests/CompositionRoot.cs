using DataGupe.Controllers;
using Moq;
using Stashbox;
using Stashbox.Mocking.Moq;

namespace DataGupe.Tests.Controllers;

internal class CompositionRoot : ICompositionRoot
{
    public void Compose(IStashboxContainer container)
    {

        container.RegisterScoped<TodoController, TodoController>();
    }
    private static StashMoq SetupMocks(IStashboxContainer container)
    {
        var stash = StashMoq.Create(container: container);

        return stash;
    }

    public static StashMoq SetupStashMoq(Action<IStashboxContainer>? testSpecificRegistration = null)
    {
#pragma warning disable IDISP001 // This gets disposed by StashMoq
#pragma warning disable IDISP004 // This gets disposed by StashMoq
        var container = new StashboxContainer().ComposeBy(typeof(CompositionRoot));
#pragma warning restore IDISP004
#pragma warning restore IDISP001

        testSpecificRegistration?.Invoke(container);

        var stash = SetupMocks(container);

        stash.Container.Validate();

        return stash;
    }
}