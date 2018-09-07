//注册命名空间
yk.core.common.registerNamespace("yk.win");

(function (ns) {
    //数组对象
    ns.openArr = new Array();
    //打开窗体
    ns.openWin = function (arr) {
        window.top.yk.core.layer.showLayer({
            title: arr.title,
            url: thisAddress + arr.url,
            width: arr.width,
            height: arr.height
        });
        ns.openArr = arr;
    }
    //关闭窗体
    ns.closeWin = function (result) {
        window.top.yk.core.layer.closeLayer(result);
    }
})(yk.win);