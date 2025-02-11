﻿using Connected.Caching;

namespace Connected.Resources.TimeLogs;
internal sealed class TimeLogCache(ICachingService cachingService)
	: CacheContainer<TimeLog, long>(cachingService, ResourcesMetaData.TimeLogKey), ITimeLogCache
{
}
