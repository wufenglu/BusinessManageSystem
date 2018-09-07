//命名空间
var globalNamespace = new Object();
//注册命名空间
globalNamespace.register = function (path) {
	var arr = path.split(".");
	var ns = "";
	for (var i = 0; i < arr.length; i++) {
		if (i > 0) ns += ".";
		ns += arr[i];
		eval("if(typeof(" + ns + ") == 'undefined') " + ns + " = new Object();");
	}
}
//注册公共
globalNamespace.register("yk.core.common");

(function (ns) {
    //公共注册命名空间
    ns.registerNamespace = function (namespace) {
        globalNamespace.register(namespace);
    }
    //分页
    ns.getPagerHtml = function (pageId, pageIndex, pageSize, recordCount, method) {
        var totalPage = 0;//总页数
        if ((recordCount / pageSize) > parseInt(recordCount / pageSize)) {
            totalPage = parseInt(recordCount / pageSize) + 1;
        } else {
            totalPage = parseInt(recordCount / pageSize);
        }
        var tempStr = "共<span style='color:red;' >" + recordCount + "</span>条记录&nbsp;分<span style='color:red;' >"
            + totalPage + "</span>页&nbsp;当前第<span style='color:red;' >" + pageIndex + "</span>页&nbsp;&nbsp;";
        if (pageIndex > 1) {
            tempStr += "<a href=\"#\" onClick=\"" + method + "('" + pageId + "'," + (1) + ")\">首页</a>&nbsp;";
        } else {
            tempStr += "首页&nbsp;";
        }
        if (pageIndex > 1) {
            tempStr += "<a href=\"#\" onClick=\"" + method + "('" + pageId + "'," + (pageIndex - 1) + ")\">上一页</a>&nbsp;"
        } else {
            tempStr += "上一页&nbsp;";
        }
        if (pageIndex < totalPage) {
            tempStr += "<a href=\"#\" onClick=\"" + method + "('" + pageId + "'," + (pageIndex + 1) + ")\">下一页</a>&nbsp;";
        } else {
            tempStr += "下一页&nbsp;";
        }
        if (pageIndex < totalPage) {
            tempStr += "<a href=\"#\" onClick=\"" + method + "('" + pageId + "'," + (totalPage) + ")\">尾页</a>&nbsp;";
        } else {
            tempStr += "尾页";
        }
        document.getElementById(pageId).innerHTML = tempStr;
    }
    //数组转换成字符串
    ns.arrayToJson = function (o) {
        var r = [];
        if (typeof o == "string") return "\"" + o.replace(/([\'\"\\])/g, "\\$1").replace(/(\n)/g, "\\n").replace(/(\r)/g, "\\r").replace(/(\t)/g, "\\t") + "\"";
        if (typeof o == "object") {
            if (!o.sort) {
                for (var i in o)
                    r.push(i + ":" + ns.arrayToJson(o[i]));
                if (!!document.all && !/^\n?function\s*toString\(\)\s*\{\n?\s*\[native code\]\n?\s*\}\n?\s*$/.test(o.toString)) {
                    r.push("toString:" + o.toString.toString());
                }
                r = "{" + r.join() + "}";
            } else {
                for (var i = 0; i < o.length; i++) {
                    r.push(ns.arrayToJson(o[i]));
                }
                r = "[" + r.join() + "]";
            }
            return r;
        }
        return o.toString();
    }
    //局部刷新，url 地址,showId 显示模块ID
    ns.get = function (url, showId) {
        var xmlHttpRequest = ns.getXmlHttpRequest();
        var returnText = "";

        xmlHttpRequest.onreadystatechange = function () {
            if (xmlHttpRequest.readyState == 4 && xmlHttpRequest.status == 200) {
                returnText = xmlHttpRequest.responseText;
            }
        }
        //open 第三个参数为是否异步请求，true为异步请求，false为同步请求
        xmlHttpRequest.open("GET", url, false);
        xmlHttpRequest.send();

        if (showId != undefined) {
            document.getElementById(showId).innerHTML = returnText;
        }
        else {
            return returnText;
        }
    }
    //局部刷新，url 地址,paras 参数列表，showId 显示模块ID
    ns.post = function (url, paras, showId) {
        var xmlHttpRequest = ns.getXmlHttpRequest();
        var returnText = "";

        xmlHttpRequest.onreadystatechange = function () {
            if (xmlHttpRequest.readyState == 4 && xmlHttpRequest.status == 200) {
                returnText = xmlHttpRequest.responseText;
            }
        }
        //open 第三个参数为是否异步请求，true为异步请求，false为同步请求
        xmlHttpRequest.open("POST", url, false);
        //xmlHttpRequest.setRequestHeader("Content-Length", paras.lenght);
        //xmlHttpRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded;");
        xmlHttpRequest.send(paras);
        if (showId != undefined) {
            document.getElementById(showId).innerHTML = returnText;
        }
        else {
            return returnText;
        }
    }
    //上传文件
    ns.postFile = function (fileId, suffix) {
        var fileObj = document.getElementById(fileId).files[0]; // 获取文件对象
        // FormData 对象
        var form = new FormData();
        form.append("file", fileObj);// 文件对象
        form.append("suffix", "jpg,png,jpeg");// 文件对象
        var msg = ns.post("/NewWeb/admin/ajax/uploadfile.ashx", form);
        var result = eval("[" + msg + "]");
        if (result[0].success) {
            return result[0].message;
        }
        else {
            alert(result[0].message);
        }
    }
    //获取 XMLHttpRequest 对象
    ns.getXmlHttpRequest = function () {
        xmlHttpRequest = null;
        if (window.ActiveXObject) {
            xmlHttpRequest = new ActiveXObject("Microsoft.XMLHTTP");
        } else if (window.XMLHttpRequest) {
            xmlHttpRequest = new XMLHttpRequest();
        }
        return xmlHttpRequest;
    }
    //去除最后一个特定字符
    ns.trimEnd = function (value, mark) {
        var splits = value.split("");
        var returnValue = "";
        if (splits[splits.length - 1] == mark) {
            for (var i = 0; i < splits.length - 1; i++) {
                returnValue += splits[i];
            }
        }
        return returnValue;
    }
    //将form里面的数据转换为json
    ns.getFormJson = function (id) {
        var json = "{";
        var inputs = document.getElementById(id).elements;
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type != null && inputs[i].type != "button" && inputs[i].type != "submit") {
                json += "\"" + inputs[i].id + "\":\"" + inputs[i].value + "\",";
            }
        }
        json = yk.trimEnd(json, ",");
        json += "}";
        return json;
    }
    //将form里面的数据转换为对象
    ns.getFormEntity = function (id) {
        var entity = [];
        var inputs = document.getElementById(id).elements;
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type != null && inputs[i].type != "button" && inputs[i].type != "submit") {
                entity[inputs[i].id] = inputs[i].value;
            }
        }
        return entity;
    }
    //加载选项卡
    ns.loadTab = function () {
        $(".tabTitle a").each(function (index) {
            if (index == 0) {
                $(".tabDiv").eq(index).css("display", "block");
                $(this).addClass("tabOn");
            }
            this.onclick = function () {
                $(".tabDiv").css("display", "none");
                $(".tabDiv").eq(index).css("display", "block");
                $(".tabTitle a").removeClass();
                $(this).addClass("tabOn");
            }
        });
    }
    //日期转换
    ns.dateConvert = function (str, format) {
        str = str.replace(/-/g, "/");
        var date = new Date(str);
        return date.Format(format);
    }
    //只允许输入数字
    ns.returnNumber=function() {
        if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode == 8) {
            return event.returnValue = true;
        }
        else {
            alert('输入错误');
            return event.returnValue = false;
        }
    }
    //不允许使用右键
    ns.mouseRightClick=function() {
        if (event.button == 2) {
            alert('不允许使用右键');
        }
    }
    //选中复选框总数
    ns.changeChkCount=function(ClassName) {
        var i = 0;
        $("." + ClassName + " input").each(function (index) {
            if (this.type == "checkbox") {
                if (this.checked == true) {
                    i++;
                }
            }
        });
        return i;
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
})(yk.core.common);

Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

$(function () {
    yk.core.common.loadTab();
});