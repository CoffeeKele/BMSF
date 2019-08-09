angular.module("scApp")
    .controller("myDeskCtrl", ['$scope', '$state', '$http', '$compile', 'dictionary', 'webUploader', 'utility',   function ($scope, $state, $http, $compile, dictionary, webUploader, utility) {

    $scope.Data = {};
    $scope.currentItem = {};
    $scope.data_In = {};
    $scope.currentResident = {};
    $scope.buttonShow = false;
    $scope.Notices = [];
    var colors = ['#0066CC', '#336600', '#FF3300'];
    $scope.aClick = function () {
        $state.go("AssignTask");
    }
    $scope.$on('renderEvent', function (evt, next, current) {
        evt.stopPropagation();
        var schdata2 = { id: next.id, title: next.title, start: next.start, color: colors[Math.ceil(Math.random() * 3) - 1] };
        $('#calendar').fullCalendar('renderEvent', schdata2, false);
        $scope.dialog.close();
    });
    $scope.$on('updateEvent', function (evt, next, current) {
        evt.stopPropagation();
        $scope.event.title = $scope.event.regName == null ? next.title : "(" + $scope.event.regName + ")" + next.title;
        var schdata2 = { title: evt.targetScope.Data.Content, start: evt.targetScope.Data.AssignDate };
        $('#calendar').fullCalendar('updateEvent', $scope.event);
        $scope.dialog.close();
    });
    $scope.$on('removeEvents', function (evt, next, current) {
        evt.stopPropagation();
        $('#calendar').fullCalendar('removeEvents', $scope.event.id);
        $scope.dialog.close();
    });
    $scope.hide = function () {
        $scope.dialog.close();
    }
    $scope.click = function (date, id) {
        var html = '<div km-include km-template="Views/Home/Remote.html" km-controller="remoteCtrl"  km-include-params="{date:\'' + date + '\',id:\'' + id + '\'}" ></div>';
        $scope.dialog = BootstrapDialog.show({
            title: '<label class=" control-label">编辑日历</label>',
            type: BootstrapDialog.TYPE_DEFAULT,
            message: html,
            size: BootstrapDialog.SIZE_WIDE,
            onshow: function (dialog) {
                var obj = dialog.getModalBody().contents();
                $compile(obj)($scope);
            }
        });
    }
    angular.element(document).ready(function () {
        $('#calendar').fullCalendar({
            customButtons: {
                myCustomButton: {
                    text: '新增提醒',
                    click: function () {
                        $scope.click(moment().format("YYYY-MM-DD HH:mm:ss"), "");
                    }
                }
            },
            header: {
                left: 'prev,next today,myCustomButton',
                center: 'title',
                right: 'month,agendaWeek'
            },
            eventClick: function (event) {
                $scope.event = event;
                $scope.click(event.start.format(), event.id);
            },
            firstDay: 1,
            editable: true,
            timeFormat: 'H(:mm)',
            eventLimit: true, // allow "more" link when too many events
            //events: {
            //    url: 'api/myDesk',
            //    error: function () {
            //        $('#script-warning').show();
            //    }
            //},
            events: function (start, end, timezone, callback) {
                $.ajax({
                    url: 'api/myDesk',
                    dataType: 'json',
                    data: {
                        // our hypothetical feed requires UNIX timestamps
                        start: start.format(),
                        end: end.format()
                    },
                    success: function (doc) {
                        var events = [];
                        //$(doc).find('event').each(function() {
                        //    events.push({
                        //        title: $(this).attr('title'),
                        //        start: $(this).attr('start') // will be parsed
                        //    });
                        //});

                        for (var i = 0; i < doc.length; i++) {
                            events.push({
                                id: doc[i].id,
                                title: doc[i].title,
                                regName: doc[i].regName,
                                color: colors[Math.ceil(Math.random() * 3) - 1],
                                start: moment(doc[i].start).format("HH:mm:ss") == "00:00:00" ? moment(doc[i].start).format("YYYY-MM-DD") : doc[i].start // will be parsed
                            });
                        }
                        callback(events);
                    }
                });
            },
            loading: function (bool) {
                $('#loading').toggle(bool);
            }
        });
    });
    $scope.init = function () {
    //    $scope.options = {
    //        buttons: [],
    //        ajaxObject: noticeRes,
    //        params: { sDate: "", eDate: "" },
    //        success: function (data) {
    //            $scope.Notices = data.Data;
    //        },
    //        pageInfo: {
    //            CurrentPage: 1,
    //            PageSize: 10
    //        }
    //    }




        //$http({
        //    url: 'api/myDesk/QueryKPI',
        //    method: 'GET'
        //}).success(function (data, header, config, status) {
        //    $scope.Data = data;

        //}).error(function (data, header, config, status) {
        //    //处理响应失败
        //    alert("Error:数据获取出错异常！")
        //});
        //$http({
        //    url: 'api/myDesk/DashboardDataIn',
        //    method: 'GET'
        //}).success(function (data, header, config, status) {
        //    var data_In = eval("(" + data + ")");

        //    initChart("container", data_In, 'column', "本年度入院人数");

        //}).error(function (data, header, config, status) {
        //    //处理响应失败
        //    alert("Error:数据获取出错异常！")
        //});
        //$http({
        //    url: 'api/myDesk/DashboardDataOut',
        //    method: 'GET'
        //}).success(function (data, header, config, status) {
        //    var data_Out = eval("(" + data + ")");
        //    initChart("container1", data_Out, 'column', "本年度结案人数");

        //}).error(function (data, header, config, status) {
        //    //处理响应失败
        //    alert("Error:数据获取出错异常！")
        //});
        //$http({
        //    url: 'api/myDesk/DashboardDataBed',
        //    method: 'GET'
        //}).success(function (data, header, config, status) {
        //    var oValue = 0;
        //    var eValue = 0;
        //    for (var i = 0; i < data.length; i++) {
        //        if (data[i].BEDSTATUS == "O") {
        //            oValue = data[i].Num;
        //        }
        //        if (data[i].BEDSTATUS == "E") {
        //            eValue = data[i].Num;
        //        }
        //    }
        //    // initChart("container2");
        //    $('#container2').highcharts({
        //        chart: {
        //            plotBackgroundColor: null,
        //            plotBorderWidth: null,
        //            plotShadow: false
        //        },
        //        title: {
        //            text: '床位占用比例'
        //        },
        //        tooltip: {
        //            pointFormat: ' <b>{point.percentage:.1f}%</b>'
        //        },
        //        exporting: {
        //            enabled: false
        //        },
        //        plotOptions: {
        //            pie: {
        //                allowPointSelect: true,
        //                cursor: 'pointer',
        //                dataLabels: {
        //                    enabled: false
        //                },
        //                showInLegend: true
        //            }
        //        },
        //        series: [{
        //            type: 'pie',

        //            data: [

        //                ['床位空闲比例', eValue],
        //                {
        //                    name: '床位占用比例',
        //                    y: oValue,
        //                    sliced: true,
        //                    selected: true,
        //                    color: '#FB9337'
        //                }
        //            ]
        //        }]
        //    });

        //}).error(function (data, header, config, status) {
        //    //处理响应失败
        //    alert("Error:数据获取出错异常！")
        //});
        //$http({
        //    url: 'api/myDesk/DashboardDataBedSore',
        //    method: 'GET'
        //}).success(function (data, header, config, status) {
        //    var data_BedSore = eval("(" + data + ")");
        //    initChart("container3", data_BedSore, 'column', "压疮");

        //}).error(function (data, header, config, status) {
        //    //处理响应失败
        //    alert("Error:数据获取出错异常！")
        //});

    }
    
    function initChart(id, data, type, title) {
        var chart;
        chart = new Highcharts.Chart({
            chart: {
                renderTo: id,          //放置图表的容器
                plotBackgroundColor: null,
                plotBorderWidth: null,
                defaultSeriesType: type   //图表类型line, spline, area, areaspline, column, bar, pie , scatter
            },
            title: {
                text: title
            },
            xAxis: {//X轴数据
                categories: ['01月', '02月', '03月', '04月', '05月', '06月', '07月', '08月', '09月', '10月', '11月', '12月'],
                labels: {
                    rotation: -40, //字体倾斜
                    align: 'right',
                    style: { font: 'normal 8px 宋体' }
                }
            },
            yAxis: {//Y轴显示文字
                title: {
                    text: ''
                },
                min: 0,
                minRange: 1
            },
            exporting: {
                enabled: false
            },
            tooltip: {
                enabled: true,
                formatter: function () {
                    return '<b>' + this.x + '</b><br/>' + this.series.name + ': ' + Highcharts.numberFormat(this.y, 0) + "个人";
                }
            },
            plotOptions: {
                column: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: true//是否显示title
                }
            },
            series: [
           {
               name: '女性',
               data: data[1]

           },
         {
             name: '男性',
             data: data[0],
             color: '#FB9337'
         }]
        });
    }

    $scope.init();

}]);














