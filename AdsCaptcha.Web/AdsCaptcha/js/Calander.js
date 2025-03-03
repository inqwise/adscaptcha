function PickerFrom_OnDateChange(sender, eventArgs) {
    var fromDate = PickerFrom.getSelectedDate();
    var toDate = PickerTo.getSelectedDate();
    CalendarFrom.setSelectedDate(fromDate);
    if (fromDate > toDate) {
        PickerTo.setSelectedDate(fromDate);
        CalendarTo.setSelectedDate(fromDate);
    }
}
function PickerTo_OnDateChange(sender, eventArgs) {
    var fromDate = PickerFrom.getSelectedDate();
    var toDate = PickerTo.getSelectedDate();
    CalendarTo.setSelectedDate(toDate);
    if (fromDate > toDate) {
        PickerFrom.setSelectedDate(toDate);
        CalendarFrom.setSelectedDate(toDate);
    }
}
function CalendarFrom_OnChange(sender, eventArgs) {
    var fromDate = CalendarFrom.getSelectedDate();
    var toDate = PickerTo.getSelectedDate();
    PickerFrom.setSelectedDate(fromDate);
    if (fromDate > toDate) {
        PickerTo.setSelectedDate(fromDate);
        CalendarTo.setSelectedDate(fromDate);
    }
}
function CalendarTo_OnChange(sender, eventArgs) {
    var fromDate = PickerFrom.getSelectedDate();
    var toDate = CalendarTo.getSelectedDate();
    PickerTo.setSelectedDate(toDate);
    if (fromDate > toDate) {
        PickerFrom.setSelectedDate(toDate);
        CalendarFrom.setSelectedDate(toDate);
    }
}
function ButtonFrom_OnClick(event) {
    if (CalendarFrom.get_popUpShowing()) {
        CalendarFrom.hide();
    } else {
        CalendarFrom.setSelectedDate(PickerFrom.getSelectedDate());
        CalendarFrom.show();
    }
}
function ButtonTo_OnClick(event) {
    if (CalendarTo.get_popUpShowing()) {
        CalendarTo.hide();
    } else {
        CalendarTo.setSelectedDate(PickerTo.getSelectedDate());
        CalendarTo.show();
    }
}
function ButtonFrom_OnMouseUp(event) {
    if (CalendarFrom.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    } else {
        return true;
    }
}
function ButtonTo_OnMouseUp(event) {
    if (CalendarTo.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    } else {
        return true;
    }
}