function editSelectedRow() {
    var grid = $("#grid").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());

    if (selectedItem === null) {
        alert('Выберите строку!');
    }
    else { grid.editRow(selectedItem); }
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
                $.post("/Users/Delete", { Id: NewId }, null, "json");
                // grid.refresh();  //  Обновления грида...
            }
        });
    }
}

var Roles = [];

function RolesFill() {

    $.get("/Users/RoleList2",
        {

        }, null, "json").done(function (data) {

            Roles = data;
            return Roles;

        }).fail(function () { alert("Ошибка загрузки данных!"); });

}

$(document).ready(function () {

    RolesFill();

    function RoleDropDownEditor(container, options) {
        debugger;
        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: false,
                dataTextField: "name",
                dataValueField: "name",
                dataSource: {
                    
                    transport: {
                        read: {
                            url: "/Users/RoleList2",
                            dataType: "json"
                        }
                    }
                }
            });
    }

 

    var dataRoles = new kendo.data.DataSource({
        transport: {    
            read: {
                url: "/Users/RoleList2",
                dataType: "json"
            }
        }
    });

    $("#RoleSearch").kendoAutoComplete({
        dataSource: dataRoles,
        dataTextField: "name",
        minLength: 1
    });

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Users/GetAll",
                dataType: "jsonp"
            },
            update: {
                url: "/Users/Edit",
                dataType: "jsonp"
            },
            destroy: {
                url: "/Users/Del",
                dataType: "jsonp"
            },
            create: {
                url: "/Users/Add",
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
                            Email: {type: "string", validation: { required: true }},
                            Password: { type: "string", validation: { required: true } },

                            Role: { type: "string", defaultValue: "Юзер"},
                            UserFio: { type: "string", validation: { required: true } },
                            Banned: { type: "boolean", validation: { required: false }, defaultValue: false }
                        }
                    }
                }
            });

    $("#grid").kendoGrid({
        dataSource: dataSource,
        navigatable: true,

        scrollable: true,
        sortable: true,
     //   editable: { mode: "inline"},
        editable: { mode: "popup", title: "Редактировать" },
       // editable: true,
        selectable: "multiple, row",  //  multiple,
     //   selectable: true,
        groupable: true,
        height: 500,

        //sort: onSorting,
        //filter: onFiltering,
        //group: onGrouping,
        //page: onPaging,

        //change: onChange,
        //dataBound: onDataBound,
        //dataBinding: onDataBinding,

        resizable: true,
        reorderable: true,

        toolbar: [{ name: "create", text: "Добавить" },
            { template: $("#destroy").html() },
            { template: $("#btnEdit").html() },
            { name: "save", text: "Сохранить" },
            { name: "cancel", text: "Отмена" },],
        //{ name: "save", text: "Сохранить" },
        //{ name: "cancel", text: "Отмена" },
        //{ template: $("#destroy").html() },
        //{ template: $("#btnEdit").html() }

        columns: [
            { field: "Id", title: "ИД", width: 50 },
            { field: "Email", title: "E-mail", width: 200 },
            { field: "Password", title: "Пароль", width: 200 },


            //{
            //    title: "Роль",
            //    field: "Role", // bound to the brandId field
            //  //  template: "#=Role#", // the template shows the name corresponding to the brandId field
            //    editor: function (container) { // use a dropdownlist as an editor
            //        // create an input element with id and name set as the bound field (brandId)
            //        var input = $('<input id="Role" name="Role">');
            //        // append to the editor container
            //        input.appendTo(container);

            //        // initialize a dropdownlist
            //        input.kendoDropDownList({
            //            dataTextField: "name",
            //            dataValueField: "name",
            //            dataSource: Roles // bind it to the brands array
            //        }).appendTo(container);
            //    }
            //},

            { field: "Role", title: "Роль", width: 200, editor: RoleDropDownEditor, template: "#= Role #" },
            { field: "UserFio", title: "Ф.И.О.", width: 200 },
            { field: "Banned", title: "Блокирован", width: 200, template: "#= Banned ? '\u2713' : '\u2610' #" }
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

   // LoadGridSetting();
});