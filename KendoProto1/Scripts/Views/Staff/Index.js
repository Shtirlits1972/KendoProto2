function editSelectedRow() {
    var grid = $("#grid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());

    if (selectedItem === null) {
        alert('Выберите строку!');
    }
    else {
        grid.editRow(selectedItem);
       // grid.refresh();
    }
}

function removeSelectedRow() {

    var grid = $("#grid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());

    if (selectedItem === null) {
        alert('Выберите строку!');
    }
    else {
        $.each($("#grid").data("kendoGrid").tbody.find(".k-state-selected"), function (index, value) {

            if (confirm("Удалить?")) {
                var grid = $("#grid").data("kendoGrid");
                var dataItem = grid.dataItem($(this).closest("tr")); // retrieve the data record for the row
                grid.dataSource.remove(dataItem);
                //var res = $("#grid").data("kendoGrid").removeRow(value);
                var NewId = value.firstChild.innerText;
                $.post("/Staff/Delete", { Id: NewId }, null, "json");
                // grid.refresh();  //  Обновления грида...
            }
        });
    }
}

function onFiltering(arg) {
    console.log("Filter on " + kendo.stringify(arg.filter));
}

$(document).ready(function () {

    var Country = kendo.data.Model.define({
        id: "Id", // the identifier of the model
        fields: {
            "CountryName": {
                type: "string"
            }
        }
    });

    function CountryEditor(container, options) {

        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: false,
                dataTextField: "CountryName",
                dataValueField: "Id",
                dataSource: {
                    transport: {
                        read: {
                            url: "/Country/GetListAsync",
                            dataType: "json"
                        }
                    }
                }
            });
    }

    function SexEditor(container, options) {

        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: false,
                dataSource: {
                    transport: {
                        read: {
                            url: "/Staff/GetSex",
                            dataType: "json"
                        }
                    }
                }
            });
    }

    function dateTimeEditor(container, options) {
        $('<input data-text-field="' + options.field +
            '" data-value-field="' + options.field +
            '" data-bind="value:' + options.field +
            '" data-format=dd.MM.yyyy"' + '" />')
            .appendTo(container)
            .kendoDateTimePicker({});
    }

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Staff/Read",
                dataType: "jsonp"
            },
            update: {
                url: "/Staff/Update",
                dataType: "jsonp"
            },
            destroy: {
                url: "/Staff/Destroy",
                dataType: "jsonp"
            },
            create: {
                url: "/Staff/Create",
                dataType: "jsonp"
            },
            parameterMap: function (options, operation) {
                if (operation !== "read" && options.models) {
                    return { models: kendo.stringify(options.models) };
                }
            }
        },
        batch: true,
        pageSize: 20,
        schema: {
            model: {
                id: "Id",
                fields: {
                    Id: { editable: false, type: "number", defaultValue: 0, nullable: false, visible: false, hidden: true },

                    StaffName: { type: "string", validation: { required: true } },
                    Birthday: { type: "date", validation: { required: true } },
                    Qty: { type: "number", validation: { required: true }, defaultValue: 0 },
                    Square: { type: "number", validation: { required: true }, defaultValue: 0 },
                    IsAdmin: { type: "boolean", validation: { required: false }, defaultValue: false },

                    Country1: {

                        fields: {
                            Country:
                            {
                                id: "Id",
                                fields: {
                                    Id: { type: "number", validation: { required: false } },
                                    CountryName: { type: "string",  validation: { required: false } }
                                }
                            }

                        }
                    },
                    Country2: {
                        id: "Id",
                        fields: {
                            Id: { validation: { required: false } },
                            CountryName: { validation: { required: false } }
                        }
                    },

                    Sex: { type: "string", validation: { required: true } },
                    Deskr: { type: "text", validation: { required: false } }
                    // Foto: { type: "string", validation: { required: true } }
                }
            }
        }
    });

    var grid = $("#grid").kendoGrid({
        dataSource: dataSource,
        navigatable: true,

        scrollable: true,
        sortable: true,
        editable: {
            mode: "popup"
            //  mode: "inline"
        },
        selectable: "multiple, row",
        groupable: true,
        height: 700,
        filter: onFiltering,
        resizable: true,
        reorderable: true,

        toolbar: [
            { name: "create", text: "Добавить" },
        //{ name: "save", text: "Сохранить" },
        //{ name: "cancel", text: "Отмена" }
       // { template: $("#btnAdd").html() },
        { template: $("#destroy").html() },
        { template: $("#btnEdit").html() }
        ],

        columns: [
            { field: "Id", title: "ИД", width: 60 },
            { field: "StaffName", title: "Ф.И.О.", width: 100 },
            { field: "Birthday", title: "Дата", width: 100, template: '#: kendo.format("{0:dd.MM.yyyy}", Birthday)#', editor: dateTimeEditor },
            { field: "Qty", title: "К-во", width: 100 },
            { field: "Square", title: "Площадь", width: 100 },
            { field: "IsAdmin", title: "Админ", width: 100, template: "#= IsAdmin ? '\u2713' : '\u2610' #" },
            {
                field: "Country1", title: "Страна-1", width: 100, template: "#= Country1.CountryName #", editor: CountryEditor,
                filterable: {
                    multi: true,
                    field: "Country1.CountryName",
                    dataSource: {
                        transport: {
                            read: {
                                url: "/Country/GetListAsync",
                                dataType: "json",
                                data: {
                                    field: "CountryName"
                                }
                            }
                        }
                    },
                    itemTemplate: function (e) {
                        if (e.field == "all") {
                            //handle the check-all checkbox template
                            return "<div><label><strong><input type='checkbox' />#= all#</strong></label></div>";
                        } else {
                            //handle the other checkboxes
                            return "<span><label><input type='checkbox' name='" + e.field + "' value='#=CountryName#'/><span>#= CountryName #</span></label></span></br>"
                        }
                    }
                },

            },
            {
                field: "Country2", title: "Страна-2", width: 100, template: "#= Country2.CountryName #", editor: CountryEditor,

                filterable: {
                    multi: true,
                    field: "Country2.CountryName",
                    dataSource: {
                        transport: {
                            read: {
                                url: "/Country/GetListAsync",
                                dataType: "json",
                                data: {
                                    field: "CountryName"
                                }
                            }
                        }
                    },
                    itemTemplate: function (e) {
                        if (e.field == "all") {
                            //handle the check-all checkbox template
                            return "<div><label><strong><input type='checkbox' />#= all#</strong></label></div>";
                        } else {
                            //handle the other checkboxes
                            return "<span><label><input type='checkbox' name='" + e.field + "' value='#=CountryName#'/><span>#= CountryName #</span></label></span></br>"
                        }
                    }
                },

            },
            {
                field: "Sex", title: "Пол", width: 100, editor: SexEditor, template: "#=Sex#",

                filterable: {
                    multi: true,
                    dataSource: [{
                        Sex: "муж",
                    }, {
                        Sex: "жен",
                    }, {
                        Sex: "не указан",
                    }],
                    checkAll: true,
                    messages: {
                        checkAll: "Все",
                        selectedItemsFormat: "Выбрано {0}"
                    }
                },
            },
            { field: "Deskr", title: "Описание", width: 100 }
            //  { field: "Foto", title: "Фото", width: 400, filterable: false }
        ],

        pageable: {
                messages: {
                    display: "{0} - {1} of {2} записей", //{0} is the index of the first record on the page, {1} - index of the last record on the page, {2} is the total amount of records
                    empty: "Нет данных",
                    page: "Страница",
                    allPages: "Все",
                    of: "из {0}", //{0} is total amount of pages
                    itemsPerPage: "записей на странице",
                    first: "1-я страница",
                    previous: "предыдущая",
                    next: "следующая",
                    last: "последняя",
                    refresh: "Обновить"
                },
            refresh: true,
            pageSizes: true,
            buttonCount: 100
        },
        groupable: {
            messages: {
                empty: "Перетащите для группировки"
            }
        },
        filterable: {
            messages: {
                info: "Это фильтр", // sets the text on top of the Filter menu
                filter: "Фильтр", // sets the text for the "Filter" button
                clear: "Отмена", // sets the text for the "Clear" button

                // when filtering boolean numbers
                isTrue: "да", // sets the text for "isTrue" radio button
                isFalse: "нет", // sets the text for "isFalse" radio button

                //changes the text of the "And" and "Or" of the Filter menu
                and: "И",
                or: "ИЛИ",
                checkAll: "Все",
                selectedItemsFormat: "Выбрано {0}"
            },
            operators: {
                //filter menu for "string" type columns
                string: {
                    eq: "равно",
                    neq: "не равно",
                    startswith: "начинается с",
                    contains: "содержит",
                    endswith: "кончается на"
                },
                //filter menu for "number" type columns
                number: {
                    eq: "равно",
                    neq: "не равно",
                    gte: "больше или равно",
                    gt: "больше чем",
                    lte: "меньше или равно",
                    lt: "меньше чем"
                },
                //filter menu for "date" type columns
                date: {
                    eq: "равно",
                    neq: "не равно",
                    gte: "позже или равно",
                    gt: "позже",
                    lte: "раньше или равно",
                    lt: "раньше"
                },
                //filter menu for foreign key values
                enums: {
                    eq: "равно",
                    neq: "не равно"
                }
            }
        }
    });
   // LoadGridSetting();
  //  grid.autoFitColumn(0);
});