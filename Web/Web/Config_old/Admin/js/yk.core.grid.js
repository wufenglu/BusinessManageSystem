yk.core.common.registerNamespace("yk.core.grid");

(function (ns) {
    //鼠标移动改变表格行的颜色
    ns.editId = 0; //编辑行对应的ID
    ns.isOperation = false; //是否可以操作快捷键
    ns.thisChangeRowIndex = 0; //当前选中行的行号,按上下键切换行
    //键按下
    ns.onkeydown = function () {
        if (event.keyCode == 32) {
            ns.isOperation = true;
        }
    }
    //编辑、添加、选中行
    ns.onkeyup = function () {
        try {
            if (ns.isOperation == true) {
                //点击键e进行编辑: space + e
                if (event.keyCode == 69) {
                    if (ns.editId > 0) {
                        update(ns.editId);
                    }
                    else {
                        alert("请选中编辑的行");
                    }
                }
                //点击键a进行添加:space + a
                if (event.keyCode == 65) {
                    add();
                }
            }
            if (event.keyCode == 32) {
                ns.isOperation = false;
            }
        } catch (e) { }
        //行选中效果
        try {
            if (event.keyCode == 40 && ns.thisChangeRowIndex < $(".gridView tr").length - 1) {
                ns.thisChangeRowIndex++;
            }
            if (event.keyCode == 38) {
                if (ns.thisChangeRowIndex > 1) //表头需要排除掉
                    ns.thisChangeRowIndex--;
            }
            $(".gridView tr").eq(ns.thisChangeRowIndex).click();
        }
        catch (e) { }
    }    
    //显示或隐藏下级
    ns.onShowOrHiddenTr=function(id, display) {
        $(".parent_" + id).each(function () {
            if (display == null) {
                //隐藏或显示下级
                if (this.style.display == "block") {
                    this.style.display = "none";
                }
                else {
                    this.style.display = "block";
                }
            }
            else {
                this.style.display = display;
            }
            //隐藏一级下级之外的所有下级，只显示子一级
            ns.onShowOrHiddenTr(this.attributes["tid"].value, "none");
        });
    }
    //获取数据源
    ns.getGridDataSource = function (id, isCk) {
        var data = [];
        $("#" + id + " tr").each(function (index) {
            if (index > 0) {
                var ckVal = $(this).find("input").attr("checked");
                if (isCk == false || (isCk == true && (ckVal == "checked" || ckVal == true))) {
                    var entity = [];
                    for (var i = 1; i < $("#" + id).find("th").length; i++) {
                        var field = $("#" + id).find("th").eq(i).attr("field");
                        entity[field] = $(this).find("td").eq(i).html();
                    }
                    data.push(entity);
                }
            }
        });
        return data;
    }
    //初始化
    ns.onload = function () {
        //列表
        $(".gridView tr").each(function () {
            this.ondblclick = function () { update(ns.editId); }
        });
        $(".gridView tr").click(function () {
            if (this.rowIndex > 0) {
                $(".gridView tr").css("background-color", "white");
                $(this).css("background-color", "#CBE2F4");
                ns.thisChangeRowIndex = this.rowIndex; // event.srcElement.parentElement.rowIndex; //this.parentElement.rowIndex;//
                var inputs = this.getElementsByTagName("input");
                ns.editId = 0; //重新赋值，确定每次重新赋值编辑的ID
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].type == "hidden") {
                        ns.editId = inputs[i].value;
                    }
                }   //this.cells[0].childNodes[0].type
            }
        });
        $(".gridView tr").mouseover(function () {
            if (event.srcElement.parentElement.rowIndex != ns.thisChangeRowIndex) {
                $(this).css("background-color", "#CBE2F4");
            }
        });
        $(".gridView tr").mouseout(function () {
            if (event.srcElement.parentElement.rowIndex != ns.thisChangeRowIndex) {
                $(this).css("background-color", "white");
            }
        });
        $(".gridView td").each(function () {
            var tdclass = this.attributes["class"];
            if (tdclass != null) {
                switch (tdclass.value) {
                    case "TdSerial":
                        break;
                    case "TdNumber":
                        break;
                    case "TdAmount":
                        break;
                    case "TdTxt":
                        break;
                    case "TdSingle":
                        break;
                    case "TdDate":
                        var tdformat = this.attributes["format"];
                        if (tdformat == null) {
                            this.innerHTML = yk.core.common.dateConvert(this.innerHTML, "yyyy-MM-dd");
                        }
                        else {
                            this.innerHTML = yk.core.common.dateConvert(this.innerHTML, tdformat.value);
                        }
                        break;
                }
            }
        });
        //树形
        $(".gridView tr").each(function () {
            if (this.attributes["tid"] != undefined) {
                this.style.display = "none";
                this.style.cursor = "pointer";
                this.ondblclick = function () { ns.onShowOrHiddenTr(this.attributes["tid"].value) };
            }
        });
        $(".parent_0").css("display", "block");
    }
    //选中复选框
    ns.changeCheckBox = function (obj) {
        $("input[type='checkbox']").attr("checked", obj.checked);
    }
})(yk.core.grid); 

$(function () {
    yk.core.grid.onload();
    document.onkeyup = function () { yk.core.grid.onkeyup(); }
    document.onkeydown = function () { yk.core.grid.onkeydown(); }
});