//注册命名空间
yk.core.common.registerNamespace("yk.core.validator");

(function (ns) {
    // JScript 文件
    //通过验证提示
    ns.ThroughTooltip = "";

    //验证
    ns.ValidatorRule = {
        Regeexp: {
            identitycode: /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])(\d{4}|\d{3}x)$/i, //身份证
            phoneormobile: /(^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$)|(^((\(\d{3}\))|(\d{3}\-))?(1[358]\d{9})$)/, //手机或电话号码     
            mobile: /^15[0123689]|18[689]|13[068]\d{8}$/, //手机
            phone: /^(([0\\+]\\d{2,3}-)?(0\\d{2,3})-)?(\\d{7,8})(-(\\d{3,}))?$/, //电话号码的函数(包括验证国内区号,国际区号,分机号)
            postalcode: /^[1-9][0-9]{5}$/, //邮政编码       
            fax: /^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/, //传真       
            email: /^[a-z\d]+(\.[a-z\d]+)*@([\da-z](-[\da-z])?)+(\.{1,2}[a-z]+)+$/, //电子邮箱        
            float: /^(\-?\d{0,8}\.?\d{0,4}|10\.?{0-4})$/, //小数   
            integer: /^[0-9]*$/, //整数
            onlynumber: /^\-?\d+\.?\d+$/, //数字
            blankerror: /\s+/, //空字符
            onlychinese: /^[\u2e80-\u9fff]+$/, //只能是中文
            hasnotchinese: /[\u2e80-\u9fff]/, //含有中文 
            dataortime: /^([1-9]\d{3}|([1-9]\d{3}\-(0?[0-9]|1[0-2])\-(0?[0-9]|[1-2]\d|3[0-1]))|([1-9]\d{3}\/(0?[0-9]|1[0-2])\/(0?[0-9]|[1-2]\d|3[0-1])))$/
        },
        //非空验证
        NotNullValidator: function (val) {
            if (val == "") {
                return false;
            }
        },
        //手机验证
        Mobile: function (val) {
            return ns.ValidatorRule.Regeexp.mobile.test(val);
        },
        //邮箱验证
        Email: function (val) {
            return ns.ValidatorRule.Regeexp.email.test(val);
        },
        //电话号码验证
        Phone: function (val) {
            return ns.ValidatorRule.Regeexp.phone.test(val);
        },
        //传真验证
        Fax: function (val) {
            return ns.ValidatorRule.Regeexp.fax.test(val);
        },
        //整数
        Integer: function (val) {
            return ns.ValidatorRule.Regeexp.integer.test(val);
        },
        //数字
        OnlyNumber: function (val) {
            return ns.ValidatorRule.Regeexp.onlynumber.test(val);
        },
        //小数
        Float: function (val) {
            return ns.ValidatorRule.Regeexp.float.test(val);
        }
    };

    //初始加载样式和验证
    ns.onload = function () {
        var marks = new Array();
        marks[0] = "input";
        marks[1] = "select";
        marks[2] = "textarea";

        var csss = new Array();
        csss[0] = "inputCss";
        csss[1] = "inputCss";
        csss[2] = "inputCss";

        for (var i = 0; i < marks.length; i++) {
            var controls = document.getElementsByTagName(marks[i]);
            for (var j = 0; j < controls.length; j++) {
                if (controls[j].attributes["GroupName"] != null) {
                    controls[j].onblur = function () { ns.checkObject(this); }
                }
                controls[j].className = csss[i];
                if (i == 0) {
                    if (controls[j].type == "button" || controls[j].type == "submit") {
                        controls[j].className = "buttonCss";
                    }
                }
            }
        }
    }
    //表单验证
    ns.VerificationForm=function(GroupName) {
        var b = true;
        $("[GroupName='" + GroupName + "']").each(function () {
            var isOk = ns.checkObject(this);
            if (isOk == false) {
                b = false;
            }
        });
        return b;
    }

    ns.checkObject=function(obj) {
        var b = true;
        var LabelID = obj.attributes["LabelID"]; //提示标签的ID     
        var ValidatorTooltip = "";

        if (obj.value == "") {
            ValidatorTooltip = "请输入" + obj.title + "！";
            b = false;
        }
        else {
            ValidatorTooltip = obj.title + "格式不正确，请重新输入！";
            switch (obj.Validator) {
                case "手机":
                    b = ns.ValidatorRule.Mobile(obj.value);
                    break;
                case "邮箱":
                    b = ns.ValidatorRule.Email(obj.value);
                    break;
                case "电话":
                    b = ns.ValidatorRule.Phone(obj.value);
                    break;
                case "传真":
                    b = ns.ValidatorRule.Fax(obj.value);
                    break;
                case "整数":
                    b = ns.ValidatorRule.Integer(obj.value);
                    break;
                case "数字":
                    b = ns.ValidatorRule.OnlyNumber(obj.value);
                    break;
                case "小数":
                    b = ns.ValidatorRule.Float(obj.value);
                    break;
            }
        }
        if (LabelID != null) {
            $("#" + LabelID.value).html("");
            $("#" + LabelID.value).css("color", "red");
        }
        if (b == false) {
            if (LabelID != null) {
                $("#" + LabelID.value).html(ValidatorTooltip);
            }
            $(obj).attr("class","validateCss");
        }
        else {
            $(obj).attr("class", "inputCss");
        }
        return b;
    }
})(yk.core.validator);

$(function () {
    yk.core.validator.onload();
});