function onChange(arg) {
    console.log('onChange');
    //var selected = $.map(this.select(), function (item) {
    //    return $(item).text();
    //});

    //kendoConsole.log("Selected: " + selected.length + " item(s), [" + selected.join(", ") + "]");
}

function onDataBound(arg) {
  //  console.log('onDataBound');
 //   kendoConsole.log("Grid data bound");
}

function onDataBinding(arg) {
    //console.log('onDataBinding');
    //SaveGridSetting();
}

function onSorting(arg) {
   // Console.log('onSorting');
    //var grid = $("#grid").data("kendoGrid");
    //localStorage["kendo-grid-options"] = kendo.stringify(grid.getOptions());
}

function onFiltering(arg) {
    console.log('onFiltering');
    //var grid = $("#grid").data("kendoGrid");
    //localStorage["kendo-grid-options"] = kendo.stringify(grid.getOptions());
}

function onPaging(arg) {
  //  Console.log('onPaging');
    //var grid = $("#grid").data("kendoGrid");
    //localStorage["kendo-grid-options"] = kendo.stringify(grid.getOptions());
}

function onGrouping(arg) {
  //  console.log('onGrouping');
    //var grid = $("#grid").data("kendoGrid");
    //localStorage["kendo-grid-options"] = kendo.stringify(grid.getOptions());
}

function SaveGridSetting()
{
    var grid = $("#grid").data("kendoGrid");

    localStorage["kendo-grid-options"] = kendo.stringify(grid.getOptions());
   // console.dir('SaveGridSetting');
}

function LoadGridSetting()
{
    var grid = $("#grid").data("kendoGrid");
    var options = localStorage["kendo-grid-options"];
   // console.dir('LoadGridSetting');
    if (options) {
        grid.setOptions(JSON.parse(options));
    }
}

function removeSelectedRow() {

    var grid = $("#grid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());

    if (selectedItem === null)
    {
        alert('Выберите строку!');
    }
    else
    {
        $.each($("#grid").data("kendoGrid").tbody.find(".k-state-selected"), function (index, value) {

            if (confirm("Удалить?"))
            {
                var grid = $("#grid").data("kendoGrid");
                var dataItem = grid.dataItem($(this).closest("tr")); // retrieve the data record for the row
                grid.dataSource.remove(dataItem);
                //var res = $("#grid").data("kendoGrid").removeRow(value);
                var NewId = value.firstChild.innerText;
                $.post("/Country/Delete", { Id: NewId }, null, "json");
               // grid.refresh();  //  Обновления грида...
            }
        });
    }
}

function editSelectedRow()
{
    var grid = $("#grid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());

    if (selectedItem === null) {
        alert('Выберите строку!');
    }
    else { grid.editRow(selectedItem); }
}

$(document).ready(function () {

        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: "/Country/Read",
                    dataType: "jsonp"
                },
                update: {
                    url: "/Country/Update",
                    dataType: "jsonp"
                },
                destroy: {
                    url: "/Country/Destroy",
                    dataType: "jsonp"
                },
                create: {
                    url: "/Country/Create",
                    dataType: "jsonp"
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            batch: true,
            pageSize: 10,
            schema: {
                model: {
                    id: "Id",
                    fields: {
                        Id: { editable: false, type: "number", defaultValue: 0, nullable: false, visible: false, hidden: true },
                        CountryName: { type: "string", validation: { required: true } }
                    }
                }
            }
    });

        $("#grid").kendoGrid({
            dataSource: dataSource,
            navigatable: true,
            
            scrollable: true,
            sortable: true,
            editable: {
                //mode: "popup"
                mode: "inline"
            },
            selectable: "multiple, row",
            groupable: true,
            height: 550,

            sort: onSorting,
            filter: onFiltering,
            group: onGrouping,
            page: onPaging,

            change: onChange,
            dataBound: onDataBound,
            dataBinding: onDataBinding,

            resizable: true,
            reorderable: true,

            toolbar: [{ name: "create", text: "Добавить" },
                { name: "save", text: "Сохранить" },
                { name: "cancel", text: "Отмена" },
                { template: $("#destroy").html() },
                { template: $("#btnEdit").html() }],

            columns: [
                { field: "Id", title: "ИД", width: 50 },
                { field: "CountryName", title: "Название", width: 400 }],
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
        }
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
            clear: "Очистить", // sets the text for the "Clear" button

            // when filtering boolean numbers
            isTrue: "да", // sets the text for "isTrue" radio button
            isFalse: "нет", // sets the text for "isFalse" radio button

            //changes the text of the "And" and "Or" of the Filter menu
            and: "И",
            or: "ИЛИ"
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

        LoadGridSetting();

});

