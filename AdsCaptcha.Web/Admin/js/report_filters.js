/*** FORM ***/

function resetForm() {
    $(".filterList").val('0');
    $(".filterListSmall").val('0');
    $(".filterID").val('');
    $(".filterName").val('');
    $(".filterDatePicker").val('');
}

function $$(name) {
    return $("[id$=" + name + "]");
}

function OnDatesFilterChange() {
    var selected = $$('listDatesFilter').val();

    var fromDate = new Date();
    var toDate = new Date();
    
    switch (selected)
    {
        case "1": // yesterday
            fromDate.dateAdd('d', -1);
            toDate = fromDate;
            $$('textFromDate').val(fromDate.toShortString());
            $$('textToDate').val(toDate.toShortString());
            break;
        case "7": // last week
            fromDate.dateAdd('d', -7);
            $$('textFromDate').val(fromDate.toShortString());
            $$('textToDate').val('');
            break;
        case "30": // last month
            fromDate.dateAdd('m', -1);
            $$('textFromDate').val(fromDate.toShortString());
            $$('textToDate').val('');
            break;
        case "0": // all time
            $(".filterDatePicker").val('');
            break;
    }
}

$(document).ready(function() {

    $(".filterDatePicker").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy',
        showAnim: 'slideDown',
        maxDate: +0
    });

    $('#resetFilters').click(function() {
        resetForm();
    });

    $('#toggleFilters').click(function() {
        $('.filterGroup').toggle('slow');
    });

});

/*** DATE ***/
function dateAddExtension(p_Interval, p_Number) {
    var thing = new String();

    p_Interval = p_Interval.toLowerCase();

    if (isNaN(p_Number)) {
        return false;
    }
    p_Number = new Number(p_Number);
    switch (p_Interval.toLowerCase()) {
        case "yyyy":
            { // year
                this.setFullYear(this.getFullYear() + p_Number);
                break;
            }
        case "q":
            { // quarter
                this.setMonth(this.getMonth() + (p_Number * 3));
                break;
            }
        case "m":
            { // month
                this.setMonth(this.getMonth() + p_Number);
                break;
            }
        case "y": // day of year
        case "d": // day
        case "w":
            { // weekday
                this.setDate(this.getDate() + p_Number);
                break;
            }
        case "ww":
            { // week of year
                this.setDate(this.getDate() + (p_Number * 7));
                break;
            }
        case "h":
            { // hour
                this.setHours(this.getHours() + p_Number);
                break;
            }
        case "n":
            { // minute
                this.setMinutes(this.getMinutes() + p_Number);
                break;
            }
        case "s":
            { // second
                this.setSeconds(this.getSeconds() + p_Number);
                break;
            }
        case "ms":
            { // second
                this.setMilliseconds(this.getMilliseconds() + p_Number);
                break;
            }
        default:
            {
                return false;
            }
    }
    return this;
}

function toShortStringExtension() {
    return this.format('dd/MM/yyyy');
}

Date.prototype.dateAdd = dateAddExtension;
Date.prototype.toShortString = toShortStringExtension;