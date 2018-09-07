
yk.core.common.registerNamespace("yk.core.layer");

(function (ns) {
    //当前选项卡id
    ns.tabId = 0;

    //初始化加载
    ns.onload = function () {
        //加载菜单	
        for (var i = 0; i < entityList.length; i++) {
            var entity = entityList[i];
            var html = "<a href=\"javascript:yk.core.layer.changeMenu(" + i + ")\">" + entity.ResourceName + "</a>";
            document.getElementById("parentMenu").innerHTML += html;
        }
        ns.changeMenu(0);

        $(".parentMenu a").each(function () {
            this.onclick = function () {
                $(".parentMenu a").removeClass("parentMenuChange");
                $(".parentMenu a").css("color", "white");
                $(this).addClass("parentMenuChange");
                $(this).css("color", "black");
            }
            this.onmouseover = function () {
                $(this).css("color", "black");
            }
            this.onmouseout = function () {
                if ($(this).attr("class") != "parentMenuChange") {
                    $(this).css("color", "white");
                }
            }
        });
        $(".parentMenu a").eq(0).click();
    }
    //加载选项
    ns.loadTab = function (i) {
        ns.showContentDiv(i);
        $(".title").css("background-color", "white");
        $(".title").eq(i).css("background-color", "yellow");
        $(".title").each(function (index) {
            this.onclick = function () { ns.loadTab(index); }
            this.ondblclick = function () { ns.remove(index); }
        });
        ns.tabId = $(".title").eq(i).attr("tabId");
    }
    //展示内容选项卡
    ns.showContentDiv = function (i) {
        $(".contentDiv").css("display", "none");
        $(".contentDiv").eq(i).css("display", "block");
    }
    //添加某一项
    ns.add = function (title, url, id) {
        ns.tabId = id;
        //如果已经存在，则显示该选项
        if ($("a[id='a" + id + "']").length > 0) {
            $("a[id='a" + id + "']").click();
            return;
        }
        var title = "<a class='title' id='a" + id + "' tabId='" + id + "'>" + title + "</a>";
        document.getElementById("titles").innerHTML += title;

        var height = document.documentElement.clientHeight - 140;//  window.innerHeight - 130; //  window.screen.availHeight - 130;
        var div = document.createElement("div");
        div.id = "div" + id;
        div.className = "contentDiv";
        div.innerHTML = "<iframe src='" + url + "' width='100%' id='frame_tab" + id + "' name='frame_tab" + id + "' frameBorder=0 noResize height='" + height + "' />"; //scrolling = no
        document.getElementById("contentDivs").appendChild(div);
        ns.loadTab($(".title").length - 1);
    }
    //移除某一项
    ns.remove = function (i) {
        $(".title").eq(i).remove();
        $(".contentDiv").eq(i).remove();
        ns.loadTab($(".title").length - 1);
    }
    //选择菜单
    ns.changeMenu = function (index) {
        var html = "";
        var entity = entityList[index];
        for (var i = 0; i < entity.ChildTree.length; i++) {
            var item = entity.ChildTree[i];
            html += "<a href=\"javascript:yk.core.layer.add('" + item.ResourceName + "','" + item.Url + "','" + item.ID + "')\" >" + item.ResourceName + "</a>";
        }
        document.getElementById("childMenu").innerHTML = html;
    }

    ns.layerIndex = 0, ns.layerWidths = [], ns.layerHeights = []; //弹出层宽、高数组
    ns.mouseStartLeft = 0.0, ns.mouseStartTop = 0.0; //鼠标开始坐标
    ns.divStartLeft = 0.0, ns.divStartTop = 0.0; //div开始坐标
    ns.isMouseDown = false; //是否鼠标按下
    ns.isDefaultButtonClick = false; //是否默认按钮点击
    //展示
    ns.showLayer = function (arr) {
        ns.layerWidths[ns.layerIndex] = arr.width;
        ns.layerHeights[ns.layerIndex] = arr.height;

        var divLock = document.createElement("Div");
        divLock.id = "layer_" + ns.layerIndex;
        divLock.className = "locklayer";
        divLock.style.cssText = "z-index:" + ns.layerIndex + ";width:" + window.innerWidth + "px;height:" + window.innerHeight + "px;";
        document.body.appendChild(divLock);

        var div = document.createElement("Div");
        div.id = "layer" + ns.layerIndex;
        div.className = "layer";
        div.title = "双击最大/最小化";
        div.style.cssText = "z-index:" + ns.layerIndex + ";width:" + arr.width + "px;height:" + arr.height + "px;"
                    + "margin-left:" + (window.innerWidth - arr.width) / 2 + "px;margin-top:" + (window.innerHeight - arr.height) / 2 + "px;";
        div.innerHTML = "<div class='layerTopBar' ondblclick='yk.core.layer.maxLayer(" + ns.layerIndex + ")'>"
                    + "<span>" + arr.title + "</span>"
                    + "<a href='javascript:void(0)' onclick='yk.core.layer.closeLayer()'>×</a>"
                    + "<a href='javascript:void(0)' onclick='yk.core.layer.maxLayer(" + ns.layerIndex + ")'>□</a></div>"
                    + "<iframe style='width:100%;height:" + (arr.height - 36) + "px' id='frame_" + ns.layerIndex + "' name='frame_" + ns.layerIndex + "' src='" + arr.url + "' frameborder='0' />";

        div.onmousemove = function () {

            if (ns.isMouseDown == true) {
                this.style.marginLeft = (ns.divStartLeft + (window.event.clientX - ns.mouseStartLeft)) + "px";
                this.style.marginTop = (ns.divStartTop + (window.event.clientY - ns.mouseStartTop)) + "px";
            }
        }
        div.onmouseup = function () {
            ns.isMouseDown = false;
        }
        div.onmousedown = function () {
            var e = e || window.event;
            ns.isMouseDown = true;
            ns.mouseStartLeft = e.clientX;
            ns.mouseStartTop = e.clientY;
            ns.divStartLeft = this.getBoundingClientRect().left;
            ns.divStartTop = this.getBoundingClientRect().top;
        }
        document.body.appendChild(div);
        ns.layerIndex++;
    }
    //关闭
    ns.closeLayer = function (result) {
        ns.layerIndex--;
        window.top.document.body.removeChild(document.getElementById("layer_" + ns.layerIndex));
        window.top.document.body.removeChild(document.getElementById("layer" + ns.layerIndex));
        ns.isDefaultButtonClick = false;
        if (result) {
            ns.getParentFrame().yk.win.openArr.onClose(result);
        }
    }
    //最大化或最小化
    ns.maxLayer = function (num) {
        //弹出层
        var layer_num = document.getElementById("layer" + num);
        if (layer_num.style.width == ns.layerWidths[num] + "px") {
            layer_num.style.width = window.innerWidth + "px";
            layer_num.style.height = window.innerHeight + "px";
            layer_num.style.marginLeft = "-2px";
            layer_num.style.marginTop = "-2px";
        }
        else {
            layer_num.style.width = ns.layerWidths[num] + "px";
            layer_num.style.height = ns.layerHeights[num] + "px";
            layer_num.style.marginLeft = (window.innerWidth - ns.layerWidths[num]) / 2 + "px";
            layer_num.style.marginTop = (window.innerHeight - ns.layerHeights[num]) / 2 + "px";
        }
    }
    //关闭并刷新父窗体
    ns.closeReloadParentLayer = function (msg) {
        if (msg) {
            alert(msg);
        }
        ns.closeLayer();
        var fram_layer = window.top.frames["frame_" + ns.layerIndex];
        if (ns.layerIndex <= 0) {
            fram_layer = ns.getParentFrame();
        }
        fram_layer.window.location = fram_layer.window.location.href;
    }
    //获取父级窗体
    ns.getParentFrame = function () {
        if (ns.layerIndex > 0) {
            return window.top.frames["frame_" + (ns.layerIndex - 1)];
        }
        else {
            return window.top.frames["frame_tab" + ns.tabId];
        }
    }
    //快捷键
    ns.onkeyup = function () {
        //点击键Esc进行撤销
        if (event.keyCode == 27 && ns.layerIndex > 0) {
            ns.closeLayer();
        }
        //回车键Enter
        if (event.keyCode == 116 || event.keyCode == 13) {
            if (ns.isDefaultButtonClick == false) {
                var inputs = document.getElementsByTagName("input");
                for (var j = 0; j < inputs.length; j++) {
                    if (inputs[j].attributes["IsEnter"] != null) {
                        if (inputs[j].attributes["IsEnter"].value.toLocaleLowerCase() == "true") {
                            inputs[j].click();
                            break;
                        }
                    }
                }
            }
        }
        switch (event.keyCode) {
            case 37:
                ns.divMove(-10, 0);
                break;
            case 38:
                ns.divMove(0, -10);
                break;
            case 39:
                ns.divMove(10, 0);
                break;
            case 40:
                ns.divMove(0, 10);
                break;
        }
    }
    //快捷键使div移动
    ns.divMove = function (left, top) {
        var obj = document.getElementById("layer" + (ns.layerIndex - 1));
        obj.style.marginLeft = obj.getBoundingClientRect().left + left + "px";
        obj.style.marginTop = obj.getBoundingClientRect().top + top + "px";
    }

})(yk.core.layer);

$(function () {
    yk.core.layer.onload();
    window.onkeyup = yk.core.layer.onkeyup();
});