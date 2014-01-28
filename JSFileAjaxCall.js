$(document).ready(function () {
    try {
        //setvariables here;
        SOWDetails.CompileTemplates(); //compile the jquery templates before binding
        SOWDetails.GetSOWInfo();    //data call here in DOM ready event
    }
    catch (Err) {
       
    }
});

var GlobalVars = {
    //list of global variables here;
};

var SOWDetails = {
    CompileTemplates: function () {
        $.template("compiledProjTemplate", $('#AssignedProjectTemplate'));
        ...
    },
    GetSOWInfo: function (value) {
        
        //clear tables below
        this.ClearSOWTables('gvFPCProjects', true);
        this.MakeAjaxCall();

    },
    MakeAjaxCall: function () {
        var sowId = $('#<id>').val();
        var year ="2014";
        $.ajax({
            url: "SOWDetails/" + sowId + "/" + year, //call WCF REST service here
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: true,
            cache: false,
            beforeSend: function () {
                SOWDetails.showProgress();
            },
            complete: function () {
                SOWDetails.hideProgress();
            },
            success: function (data) {
                SOWDetails.loadSOWTemplate(data);
                SOWDetails.setCellColor();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var responseText = jqXHR.responseText;
                if (typeof (responseText) != 'undefined' &&
                            (responseText.toString().toLowerCase().indexOf("<any text>") >= 0) {
                    SOWDetails.RedirectOnTimeout(); //handle timeout if any here
                }
                else if (jqXHR == null || textStatus === 'timeout') { //handle session expire here
                    alert('Session expired or timed out: Restart Browser');
                    SOWDetails.RedirectOnTimeout();
                    return false;
                }
                else {
                    //handle request error here
                    alert("Request Error /MakeAjaxCall/. \njqXHR: " + jqXHR + "\n textStatus: " + textStatus + "\n errorThrown: " + errorThrown);
                    var params = "";
                    SOWDetails.SendMailOnException(params);
                }
            }
        });
    },
    SendMailOnException: function (errorThrown) {
        
    },
    RedirectOnTimeout: function () {
        
    },
    SetProjectName: function (ProjectID, ProjectName) {
        
    },
    loadSOWTemplate: function (data) {
      ...
      ...
        var tblForecastInfo = $('#tblSOW tbody');
        var tBodySOWProjects = $('#gvFPCProjects tbody');
        var tblTotal = $('#tblTotal tbody');

        /* bind each template with array */
        if (data.fpcForecast.length > 0) {
            var fpcForecast = data.fpcForecast;
            $.tmpl("compiledForecastTemplate", fpcForecast).appendTo(tblForecastInfo);
            $('#lblSOWNo').text(fpcForecast[0].SOWID);
            ...
            ...
        }
        else {
            $('#NoRecordsTemplate').tmpl({ 'Message': 'No records Found.', 'ColSpan': '13' }).appendTo(tblForecastInfo);
        }
        //apply styles below after rendering
        $('#tblProjects tr').addClass("gridRowStyleSOW");
        this.HandleRedirectionAndStyles();
        
        //set table width if required as below
        this.SetCellWidthBasedOnHeader();
    },
    SetCellWidthBasedOnHeader: function () {
        var arr = new Array();
        $('#tblHeader tr th').each(function (i) {
            arr.push($(this).css("width"));
        });

        if ($('#tblSOW tr td').length > 1) {
            $('#tblSOW tr td').each(function (i) {
                $(this).css("width", arr[i].toString());
            });
        }

        if ($('#gvFPCProjects tr td').length > 1) {
            $('#gvFPCProjects tr').each(function () {
                $('td', this).each(function (i) {
                    $(this).css("width", arr[i].toString());
                });
            });
        }

        if ($('#tblTotal tr:first td').length > 0) {
            $('#tblTotal tr:first td:nth-child(2)').css("width", arr[3].toString());
            $('#tblTotal tr:first td:nth-child(3)').css("width", arr[4].toString());
            $('#tblTotal tr:first td:nth-child(4)').css("width", arr[5].toString());
            $('#tblTotal tr:first td:nth-child(5)').css("width", arr[6].toString());
            $('#tblTotal tr:first td:nth-child(6)').css("width", arr[7].toString());
            $('#tblTotal tr:first td:nth-child(7)').css("width", arr[8].toString());
            $('#tblTotal tr:first td:nth-child(8)').css("width", arr[9].toString());
            $('#tblTotal tr:first td:nth-child(9)').css("width", arr[10].toString());
            $('#tblTotal tr:first td:nth-child(10)').css("width", arr[11].toString());
            $('#tblTotal tr:first td:nth-child(11)').css("width", arr[12].toString());
            $('#tblTotal tr:first td:nth-child(12)').css("width", arr[13].toString());
            $('#tblTotal tr:first td:nth-child(13)').css("width", arr[14].toString());
            $('#tblTotal tr:first td:nth-child(14)').css("width", arr[15].toString());
            $('#tblTotal tr:first td:nth-child(15)').css("width", arr[16].toString());
            $('#tblTotal tr:first td:nth-child(16)').css("width", arr[17].toString());
            $('#tblTotal tr:first td:nth-child(17)').css("width", arr[18].toString());
        }
        if ($('#tblTotal tr:last td').length > 0) {
            $('#tblTotal tr:last td:nth-child(2)').css("width", arr[3].toString());
            $('#tblTotal tr:last td:nth-child(3)').css("width", arr[4].toString());
            $('#tblTotal tr:last td:nth-child(4)').css("width", arr[5].toString());
            $('#tblTotal tr:last td:nth-child(5)').css("width", arr[6].toString());
            $('#tblTotal tr:last td:nth-child(6)').css("width", arr[7].toString());
            $('#tblTotal tr:last td:nth-child(7)').css("width", arr[8].toString());
            $('#tblTotal tr:last td:nth-child(8)').css("width", arr[9].toString());
            $('#tblTotal tr:last td:nth-child(9)').css("width", arr[10].toString());
            $('#tblTotal tr:last td:nth-child(10)').css("width", arr[11].toString());
            $('#tblTotal tr:last td:nth-child(11)').css("width", arr[12].toString());
            $('#tblTotal tr:last td:nth-child(12)').css("width", arr[13].toString());
            $('#tblTotal tr:last td:nth-child(13)').css("width", arr[14].toString());
            $('#tblTotal tr:last td:nth-child(14)').css("width", arr[15].toString());
            $('#tblTotal tr:last td:nth-child(15)').css("width", arr[16].toString());
            $('#tblTotal tr:last td:nth-child(16)').css("width", arr[17].toString());
            $('#tblTotal tr:last td:nth-child(17)').css("width", arr[18].toString());
        }
    },
    HandleRedirectionAndStyles: function () {
        if (GlobalVars.Page == '<anypagename>') {
            $('#<id>').css('height', '350px');
            $('#<id>').css('cursor', 'auto').css('textDecoration', 'none');
            document.getElementById('<id>').onclick = function () {
                return false;
            }
        }
    },
    formatCurrency: function (num) {
        //num = (isNaN(parseFloat(num))) ? '0.00' : num;
        num = (isNaN(parseFloat(num))) ? '' : num;
        if (num == '') return num;
        num = parseFloat(num).toFixed(2);
        var sign = (num < 0) ? '-' : '';
        var a = num.split('.', 2);
        var d = a[1];
        var i = a[0];                       //modified to display positive/negative nos.
        var isPositive = (i == Math.abs(parseInt(i)));

        var n = new String(i);
        if (n.indexOf('-') >= 0)
            n = n.replace('-', '');
        var b = [];
        while (n.length > 3) {
            var nn = n.substr(n.length - 3);
            b.unshift(nn);
            n = n.substr(0, n.length - 3);
        }
        if (n.length > 0) { b.unshift(n); }
        n = b.join(GlobalVars.digitGroupSymbol);
        num = n + '.' + d;

        // format symbol/negative
        var format = isPositive ? GlobalVars.positiveFormat : GlobalVars.negativeFormat;
        var money = format.replace(/%s/g, GlobalVars.symbol);
        money = money.replace(/%n/g, num).replace('-', '');
        return money;
    },
    setCellColor: function () {
        $('#tblTotal tr:last td').each(function (i) {
            var cell = $(this)[0];
            if (i >= 1 && GlobalVars.DoFormat) {
                var replacedString = cell.innerText.replace(/,/g, '').replace('$', ''); //if -ve, retain black color.
                if (parseInt(replacedString) > 0)
                    $(cell).css('color', 'red');
            }
        });
    },
    hideProgress: function () {
        $("#<Loading>").css("display", "none");
    },
    showProgress: function () {
        $("#<Loading>").css("display", "block");
    },
    ClearSOWTables: function (id, applyTrigger) {
        $('#' + id + ' tr').remove();
        if (applyTrigger)
            $('#' + id).trigger("update");
    }
};

