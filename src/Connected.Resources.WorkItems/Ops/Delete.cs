﻿using Connected.Entities;
using Connected.Notifications;
using Connected.Services;
using Connected.Storage;

namespace Connected.Resources.WorkItems.Ops;
internal sealed class Delete(IStorageProvider storage, IWorkItemService workItems, IEventService events, IWorkItemCache cache)
	: ServiceAction<IPrimaryKeyDto<long>>
{
	protected override async Task OnInvoke()
	{
		_ = SetState(await workItems.Select(Dto)) as WorkItem ?? throw new NullReferenceException(Strings.ErrEntityExpected);

		await storage.Open<WorkItem>().Update(Dto.AsEntity<WorkItem>(State.Deleted));
		await cache.Remove(Dto.Id);
		await events.Deleted(this, workItems, Dto.Id);
	}
}
