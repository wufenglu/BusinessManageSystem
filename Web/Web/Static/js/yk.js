
//局部刷新方法
var yk = {
    //局部刷新，url 地址,showId 显示模块ID
    get: function (url, showId) {
        xmlHttpRequest = yk.getXmlHttpRequest();
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
    },
    //局部刷新，url 地址,paras 参数列表，showId 显示模块ID
    post: function (url, paras, showId) {
        xmlHttpRequest = yk.getXmlHttpRequest();
        var returnText = "";

        xmlHttpRequest.onreadystatechange = function () {
            if (xmlHttpRequest.readyState == 4 && xmlHttpRequest.status == 200) {
                returnText = xmlHttpRequest.responseText;
            }
        }
        //open 第三个参数为是否异步请求，true为异步请求，false为同步请求
        xmlHttpRequest.open("POST", url, false);
        xmlHttpRequest.setRequestHeader("Content-Length", paras.lenght);
        xmlHttpRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded;");
        xmlHttpRequest.send(paras);
        if (showId != undefined) {
            document.getElementById(showId).innerHTML = returnText;
        }
        else {
            return returnText;
        }
    },
    //局部刷新，url 地址,paras 参数列表，showId 显示模块ID
    postMethod: function (url, paras, showId) {
        xmlHttpRequest = yk.getXmlHttpRequest();
        var returnText = "";

        xmlHttpRequest.onreadystatechange = function () {
            if (xmlHttpRequest.readyState == 4 && xmlHttpRequest.status == 200) {
                returnText = xmlHttpRequest.responseText;
            }
        }
        //open 第三个参数为是否异步请求，true为异步请求，false为同步请求
        xmlHttpRequest.open("POST", url, false);
        xmlHttpRequest.setRequestHeader("Content-Length", paras.lenght);
        xmlHttpRequest.setRequestHeader("Content-Type", "application/json;charset=utf-8");
        xmlHttpRequest.setRequestHeader("Data-Type", "json");
        xmlHttpRequest.send(paras);
        if (showId != undefined) {
            document.getElementById(showId).innerHTML = returnText;
        }
        else {
            return returnText;
        }
    },
    //局部刷新，method 方法名,paras 参数列表
    postThis: function (method, paras) {
        var url = window.location + "/" + method;
        var msg = yk.postMethod(url, paras);
        var obj = eval("[" + msg + "]");
        return obj.d;
    },
    //获取 XMLHttpRequest 对象
    getXmlHttpRequest: function () {
        xmlHttpRequest = null;
        if (window.ActiveXObject) {
            xmlHttpRequest = new ActiveXObject("Microsoft.XMLHTTP");
        } else if (window.XMLHttpRequest) {
            xmlHttpRequest = new XMLHttpRequest();
        }
        return xmlHttpRequest;
    },
    getValueById: function (id) {
        return document.getElementById(id).value;
    },
    trimEnd: function (value, mark) {
        var splits = value.split("");
        var returnValue = "";
        if (splits[splits.length - 1] == mark) {
            for (var i = 0; i < splits.length - 1; i++) {
                returnValue += splits[i];
            }
        }
        return returnValue;
    },
    getFormJson: function (id) {
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
    },
    getFormEntity: function (id) {
        var entity = [];
        var inputs = document.getElementById(id).elements;
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type != null && inputs[i].type != "button" && inputs[i].type != "submit") {
                entity[inputs[i].id] = inputs[i].value;
            }
        }
        return entity;
    }
};