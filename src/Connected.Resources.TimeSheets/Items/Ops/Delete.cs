﻿using Connected.Entities;
using Connected.Notifications;
using Connected.Services;
using Connected.Storage;

namespace Connected.Resources.TimeSheets.Items.Ops;
internal sealed class Delete(IStorageProvider storage, IEventService events, ITimeSheetItemService items, ITimeSheetItemCache cache)
	: ServiceAction<IPrimaryKeyDto<int>>
{
	protected override async Task OnInvoke()
	{
		_ = SetState(await items.Select(Dto)) as TimeSheetItem ?? throw new NullReferenceException(Strings.ErrEntityExpected);

		await storage.Open<TimeSheetItem>().Update(Dto.AsEntity<TimeSheetItem>(State.Deleted));
		await cache.Remove(Dto.Id);
		await events.Deleted(this, items, Dto.Id);
	}
}
