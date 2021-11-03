using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newbe.BookmarkManager.Services.Servers;

namespace Newbe.BookmarkManager.Services
{
    public class SyncTagRelatedBkCountJob : ISyncTagRelatedBkCountJob
    {
        private readonly ILogger<SyncTagRelatedBkCountJob> _logger;
        private readonly IBkManagerClient _bkManager;
        private readonly ITagsManager _tagsManager;

        // ReSharper disable once NotAccessedField.Local
        private IDisposable _jobHandler;

        public SyncTagRelatedBkCountJob(
            ILogger<SyncTagRelatedBkCountJob> logger,
            IBkManagerClient bkManager,
            ITagsManager tagsManager)
        {
            _logger = logger;
            _bkManager = bkManager;
            _tagsManager = tagsManager;
        }

        public ValueTask StartAsync()
        {
            _jobHandler = new[] { 1L }.ToObservable()
                .Concat(Observable.Interval(TimeSpan.FromMinutes(10)))
                .Buffer(TimeSpan.FromSeconds(5), 50)
                .Where(x => x.Count > 0)
                .Select(x => x.First())
                .Select(_ => Observable.FromAsync(async () =>
                {
                    try
                    {
                        await RunSyncAsync();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Failed");
                    }
                }))
                .Concat()
                .Subscribe();
            return ValueTask.CompletedTask;
        }

        private async Task RunSyncAsync()
        {
            var counts = await _bkManager.GetTagRelatedCountAsync();
            await _tagsManager.UpdateRelatedCountAsync(counts);
        }
    }
}