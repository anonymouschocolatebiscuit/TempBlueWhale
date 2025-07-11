﻿/**
* jQuery ligerUI 1.2.4
* 
* http://ligerui.com
*  
* Author daomi 2014 [ gd_star@163.com ] 
* 
*/
(function ($) {
    $.fn.ligerDateEditor = function () {
        return $.ligerui.run.call(this, "ligerDateEditor", arguments);
    };

    $.fn.ligerGetDateEditorManager = function () {
        return $.ligerui.run.call(this, "ligerGetDateEditorManager", arguments);
    };

    $.ligerDefaults.DateEditor = {
        format: "yyyy-MM-dd hh:mm",
        width: null,
        showTime: false,
        onChangeDate: false,
        absolute: true,
        cancelable: true,
        readonly: false
    };
    $.ligerDefaults.DateEditorString = {
        dayMessage: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
        monthMessage: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
        todayMessage: "Today",
        closeMessage: "Close"
    };
    $.ligerMethos.DateEditor = {};

    $.ligerui.controls.DateEditor = function (element, options) {
        $.ligerui.controls.DateEditor.base.constructor.call(this, element, options);
    };
    $.ligerui.controls.DateEditor.ligerExtend($.ligerui.controls.Input, {
        __getType: function () {
            return 'DateEditor';
        },
        __idPrev: function () {
            return 'DateEditor';
        },
        _extendMethods: function () {
            return $.ligerMethos.DateEditor;
        },
        _render: function () {
            var g = this, p = this.options;
            if (!p.showTime && p.format.indexOf(" hh:mm") > -1)
                p.format = p.format.replace(" hh:mm", "");
            if (this.element.tagName.toLowerCase() != "input" || this.element.type != "text")
                return;
            g.inputText = $(this.element);
            if (!g.inputText.hasClass("l-text-field"))
                g.inputText.addClass("l-text-field");
            g.link = $('<div class="l-trigger"><div class="l-trigger-icon"></div></div>');
            g.text = g.inputText.wrap('<div class="l-text l-text-date"></div>').parent();
            g.text.append('<div class="l-text-l"></div><div class="l-text-r"></div>');
            g.text.append(g.link);
            g.textwrapper = g.text.wrap('<div class="l-text-wrapper"></div>').parent();
            var dateeditorHTML = "";
            dateeditorHTML += "<div class='l-box-dateeditor' style='display:none'>";
            dateeditorHTML += "    <div class='l-box-dateeditor-header'>";
            dateeditorHTML += "        <div class='l-box-dateeditor-header-btn l-box-dateeditor-header-prevyear'><span></span></div>";
            dateeditorHTML += "        <div class='l-box-dateeditor-header-btn l-box-dateeditor-header-prevmonth'><span></span></div>";
            dateeditorHTML += "        <div class='l-box-dateeditor-header-text'><a class='l-box-dateeditor-header-month'></a> , <a  class='l-box-dateeditor-header-year'></a></div>";
            dateeditorHTML += "        <div class='l-box-dateeditor-header-btn l-box-dateeditor-header-nextmonth'><span></span></div>";
            dateeditorHTML += "        <div class='l-box-dateeditor-header-btn l-box-dateeditor-header-nextyear'><span></span></div>";
            dateeditorHTML += "    </div>";
            dateeditorHTML += "    <div class='l-box-dateeditor-body'>";
            dateeditorHTML += "        <table cellpadding='0' cellspacing='0' border='0' class='l-box-dateeditor-calendar'>";
            dateeditorHTML += "            <thead>";
            dateeditorHTML += "                <tr><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td></tr>";
            dateeditorHTML += "            </thead>";
            dateeditorHTML += "            <tbody>";
            dateeditorHTML += "                <tr class='l-first'><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td></tr><tr><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td></tr><tr><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td></tr><tr><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td></tr><tr><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td></tr><tr><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td></tr>";
            dateeditorHTML += "            </tbody>";
            dateeditorHTML += "        </table>";
            dateeditorHTML += "        <ul class='l-box-dateeditor-monthselector'><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>";
            dateeditorHTML += "        <ul class='l-box-dateeditor-yearselector'><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>";
            dateeditorHTML += "        <ul class='l-box-dateeditor-hourselector'><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>";
            dateeditorHTML += "        <ul class='l-box-dateeditor-minuteselector'><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li><li></li></ul>";
            dateeditorHTML += "    </div>";
            dateeditorHTML += "    <div class='l-box-dateeditor-toolbar'>";
            dateeditorHTML += "        <div class='l-box-dateeditor-time'></div>";
            dateeditorHTML += "        <div class='l-button l-button-today'></div>";
            dateeditorHTML += "        <div class='l-button l-button-close'></div>";
            dateeditorHTML += "        <div class='l-clear'></div>";
            dateeditorHTML += "    </div>";
            dateeditorHTML += "</div>";
            g.dateeditor = $(dateeditorHTML);
            if (p.absolute)
                g.dateeditor.appendTo('body').addClass("l-box-dateeditor-absolute");
            else
                g.textwrapper.append(g.dateeditor);
            g.header = $(".l-box-dateeditor-header", g.dateeditor);
            g.body = $(".l-box-dateeditor-body", g.dateeditor);
            g.toolbar = $(".l-box-dateeditor-toolbar", g.dateeditor);

            g.body.thead = $("thead", g.body);
            g.body.tbody = $("tbody", g.body);
            g.body.monthselector = $(".l-box-dateeditor-monthselector", g.body);
            g.body.yearselector = $(".l-box-dateeditor-yearselector", g.body);
            g.body.hourselector = $(".l-box-dateeditor-hourselector", g.body);
            g.body.minuteselector = $(".l-box-dateeditor-minuteselector", g.body);

            g.toolbar.time = $(".l-box-dateeditor-time", g.toolbar);
            g.toolbar.time.hour = $("<a></a>");
            g.toolbar.time.minute = $("<a></a>");
            g.buttons = {
                btnPrevYear: $(".l-box-dateeditor-header-prevyear", g.header),
                btnNextYear: $(".l-box-dateeditor-header-nextyear", g.header),
                btnPrevMonth: $(".l-box-dateeditor-header-prevmonth", g.header),
                btnNextMonth: $(".l-box-dateeditor-header-nextmonth", g.header),
                btnYear: $(".l-box-dateeditor-header-year", g.header),
                btnMonth: $(".l-box-dateeditor-header-month", g.header),
                btnToday: $(".l-button-today", g.toolbar),
                btnClose: $(".l-button-close", g.toolbar)
            };
            var nowDate = new Date();
            g.now = {
                year: nowDate.getFullYear(),
                month: nowDate.getMonth() + 1,
                day: nowDate.getDay(),
                date: nowDate.getDate(),
                hour: nowDate.getHours(),
                minute: nowDate.getMinutes()
            };
            g.currentDate = {
                year: nowDate.getFullYear(),
                month: nowDate.getMonth() + 1,
                day: nowDate.getDay(),
                date: nowDate.getDate(),
                hour: nowDate.getHours(),
                minute: nowDate.getMinutes()
            };
            g.selectedDate = null;
            g.usedDate = null;

            $("td", g.body.thead).each(function (i, td) {
                $(td).html(p.dayMessage[i]);
            });
            $("li", g.body.monthselector).each(function (i, li) {
                $(li).html(p.monthMessage[i]);
            });
            g.buttons.btnToday.html(p.todayMessage);
            g.buttons.btnClose.html(p.closeMessage);
            if (p.showTime) {
                g.toolbar.time.show();
                g.toolbar.time.append(g.toolbar.time.hour).append(":").append(g.toolbar.time.minute);
                $("li", g.body.hourselector).each(function (i, item) {
                    var str = i;
                    if (i < 10) str = "0" + i.toString();
                    $(this).html(str);
                });
                $("li", g.body.minuteselector).each(function (i, item) {
                    var str = i;
                    if (i < 10) str = "0" + i.toString();
                    $(this).html(str);
                });
            }
            g.bulidContent();
            if (p.initValue) {
                g.inputText.val(p.initValue);
            }
            if (g.inputText.val() != "") {
                g.onTextChange();
            }
            g.dateeditor.hover(null, function (e) {
                if (g.dateeditor.is(":visible") && !g.editorToggling) {
                    g.toggleDateEditor(true);
                }
            });
            g.link.hover(function () {
                if (p.disabled) return;
                this.className = "l-trigger-hover";
            }, function () {
                if (p.disabled) return;
                this.className = "l-trigger";
            }).mousedown(function () {
                if (p.disabled) return;
                this.className = "l-trigger-pressed";
            }).mouseup(function () {
                if (p.disabled) return;
                this.className = "l-trigger-hover";
            }).click(function () {
                if (p.disabled) return;
                g.bulidContent();
                g.toggleDateEditor(g.dateeditor.is(":visible"));
            });
            if (p.disabled) {
                g.inputText.attr("readonly", "readonly");
                g.text.addClass('l-text-disabled');
            }
            g.buttons.btnClose.click(function () {
                g.toggleDateEditor(true);
            });
            $("td", g.body.tbody).hover(function () {
                if ($(this).hasClass("l-box-dateeditor-today")) return;
                $(this).addClass("l-box-dateeditor-over");
            }, function () {
                $(this).removeClass("l-box-dateeditor-over");
            }).click(function () {
                $(".l-box-dateeditor-selected", g.body.tbody).removeClass("l-box-dateeditor-selected");
                if (!$(this).hasClass("l-box-dateeditor-today"))
                    $(this).addClass("l-box-dateeditor-selected");
                g.currentDate.date = parseInt($(this).html());
                g.currentDate.day = new Date(g.currentDate.year, g.currentDate.month - 1, 1).getDay();
                if ($(this).hasClass("l-box-dateeditor-out")) {
                    if ($("tr", g.body.tbody).index($(this).parent()) == 0) {
                        if (--g.currentDate.month == 0) {
                            g.currentDate.month = 12;
                            g.currentDate.year--;
                        }
                    } else {
                        if (++g.currentDate.month == 13) {
                            g.currentDate.month = 1;
                            g.currentDate.year++;
                        }
                    }
                }
                g.selectedDate = {
                    year: g.currentDate.year,
                    month: g.currentDate.month,
                    date: g.currentDate.date
                };
                g.showDate();
                g.editorToggling = true;
                g.dateeditor.slideToggle('fast', function () {
                    g.editorToggling = false;
                });
            });

            $(".l-box-dateeditor-header-btn", g.header).hover(function () {
                $(this).addClass("l-box-dateeditor-header-btn-over");
            }, function () {
                $(this).removeClass("l-box-dateeditor-header-btn-over");
            });
            g.buttons.btnYear.click(function () {
                if (!g.body.yearselector.is(":visible")) {
                    $("li", g.body.yearselector).each(function (i, item) {
                        var currentYear = g.currentDate.year + (i - 4);
                        if (currentYear == g.currentDate.year)
                            $(this).addClass("l-selected");
                        else
                            $(this).removeClass("l-selected");
                        $(this).html(currentYear);
                    });
                }

                g.body.yearselector.slideToggle();
            });
            g.body.yearselector.hover(function () { }, function () {
                $(this).slideUp();
            });
            $("li", g.body.yearselector).click(function () {
                g.currentDate.year = parseInt($(this).html());
                g.body.yearselector.slideToggle();
                g.bulidContent();
            });
            g.buttons.btnMonth.click(function () {
                $("li", g.body.monthselector).each(function (i, item) {
                    if (g.currentDate.month == i + 1)
                        $(this).addClass("l-selected");
                    else
                        $(this).removeClass("l-selected");
                });
                g.body.monthselector.slideToggle();
            });
            g.body.monthselector.hover(function () { }, function () {
                $(this).slideUp("fast");
            });
            $("li", g.body.monthselector).click(function () {
                var index = $("li", g.body.monthselector).index(this);
                g.currentDate.month = index + 1;
                g.body.monthselector.slideToggle();
                g.bulidContent();
            });

            g.toolbar.time.hour.click(function () {
                $("li", g.body.hourselector).each(function (i, item) {
                    if (g.currentDate.hour == i)
                        $(this).addClass("l-selected");
                    else
                        $(this).removeClass("l-selected");
                });
                g.body.hourselector.slideToggle();
            });
            g.body.hourselector.hover(function () { }, function () {
                $(this).slideUp("fast");
            });
            $("li", g.body.hourselector).click(function () {
                var index = $("li", g.body.hourselector).index(this);
                g.currentDate.hour = index;
                g.body.hourselector.slideToggle();
                g.bulidContent();
                g.showDate();
            });
            g.toolbar.time.minute.click(function () {
                $("li", g.body.minuteselector).each(function (i, item) {
                    if (g.currentDate.minute == i)
                        $(this).addClass("l-selected");
                    else
                        $(this).removeClass("l-selected");
                });
                g.body.minuteselector.slideToggle("fast", function () {
                    var index = $("li", this).index($('li.l-selected', this));
                    if (index > 29) {
                        var offSet = ($('li.l-selected', this).offset().top - $(this).offset().top);
                        $(this).animate({ scrollTop: offSet });
                    }
                });
            });
            g.body.minuteselector.hover(function () { }, function () {
                $(this).slideUp("fast");
            });
            $("li", g.body.minuteselector).click(function () {
                var index = $("li", g.body.minuteselector).index(this);
                g.currentDate.minute = index;
                g.body.minuteselector.slideToggle("fast");
                g.bulidContent();
                g.showDate();
            });

            g.buttons.btnPrevMonth.click(function () {
                if (--g.currentDate.month == 0) {
                    g.currentDate.month = 12;
                    g.currentDate.year--;
                }
                g.bulidContent();
            });
            g.buttons.btnNextMonth.click(function () {
                if (++g.currentDate.month == 13) {
                    g.currentDate.month = 1;
                    g.currentDate.year++;
                }
                g.bulidContent();
            });
            g.buttons.btnPrevYear.click(function () {
                g.currentDate.year--;
                g.bulidContent();
            });
            g.buttons.btnNextYear.click(function () {
                g.currentDate.year++;
                g.bulidContent();
            });
            g.buttons.btnToday.click(function () {
                g.currentDate = {
                    year: g.now.year,
                    month: g.now.month,
                    day: g.now.day,
                    date: g.now.date
                };
                g.selectedDate = {
                    year: g.now.year,
                    month: g.now.month,
                    day: g.now.day,
                    date: g.now.date
                };
                g.showDate();
                g.dateeditor.slideToggle("fast");
            });
            g.inputText.change(function () {
                g.onTextChange();
            }).blur(function () {
                g.text.removeClass("l-text-focus");
            }).focus(function () {
                g.text.addClass("l-text-focus");
            });
            g.text.hover(function () {
                g.text.addClass("l-text-over");
            }, function () {
                g.text.removeClass("l-text-over");
            });
            if (p.label) {
                g.labelwrapper = g.textwrapper.wrap('<div class="l-labeltext"></div>').parent();
                g.labelwrapper.prepend('<div class="l-text-label" style="float:left;display:inline;">' + p.label + ':&nbsp</div>');
                g.textwrapper.css('float', 'left');
                if (!p.labelWidth) {
                    p.labelWidth = $('.l-text-label', g.labelwrapper).outerWidth();
                } else {
                    $('.l-text-label', g.labelwrapper).outerWidth(p.labelWidth);
                }
                $('.l-text-label', g.labelwrapper).width(p.labelWidth);
                $('.l-text-label', g.labelwrapper).height(g.text.height());
                g.labelwrapper.append('<br style="clear:both;" />');
                if (p.labelAlign) {
                    $('.l-text-label', g.labelwrapper).css('text-align', p.labelAlign);
                }
                g.textwrapper.css({ display: 'inline' });
                g.labelwrapper.width(g.text.outerWidth() + p.labelWidth + 2);
            }

            g.set(p);
            $(document).bind("click.dateeditor", function (e) {
                if (g.dateeditor.is(":visible") && $((e.target || e.srcElement)).closest(".l-box-dateeditor, .l-text-date").length == 0) {
                    g.toggleDateEditor(true);
                }
            });
        },
        destroy: function () {
            if (this.textwrapper) this.textwrapper.remove();
            if (this.dateeditor) this.dateeditor.remove();
            this.options = null;
            $.ligerui.remove(this);
        },
        bulidContent: function () {
            var g = this, p = this.options;
            var thismonthFirstDay = new Date(g.currentDate.year, g.currentDate.month - 1, 1).getDay();
            var nextMonth = g.currentDate.month;
            var nextYear = g.currentDate.year;
            if (++nextMonth == 13) {
                nextMonth = 1;
                nextYear++;
            }
            var monthDayNum = new Date(nextYear, nextMonth - 1, 0).getDate();
            var prevMonthDayNum = new Date(g.currentDate.year, g.currentDate.month - 1, 0).getDate();

            g.buttons.btnMonth.html(p.monthMessage[g.currentDate.month - 1]);
            g.buttons.btnYear.html(g.currentDate.year);
            g.toolbar.time.hour.html(g.currentDate.hour);
            g.toolbar.time.minute.html(g.currentDate.minute);
            if (g.toolbar.time.hour.html().length == 1)
                g.toolbar.time.hour.html("0" + g.toolbar.time.hour.html());
            if (g.toolbar.time.minute.html().length == 1)
                g.toolbar.time.minute.html("0" + g.toolbar.time.minute.html());
            $("td", this.body.tbody).each(function () { this.className = "" });
            $("tr", this.body.tbody).each(function (i, tr) {
                $("td", tr).each(function (j, td) {
                    var id = i * 7 + (j - thismonthFirstDay);
                    var showDay = id + 1;
                    if (g.selectedDate && g.currentDate.year == g.selectedDate.year &&
                        g.currentDate.month == g.selectedDate.month &&
                        id + 1 == g.selectedDate.date) {
                        if (j == 0 || j == 6) {
                            $(td).addClass("l-box-dateeditor-holiday")
                        }
                        $(td).addClass("l-box-dateeditor-selected");
                        $(td).siblings().removeClass("l-box-dateeditor-selected");
                    }
                    else if (g.currentDate.year == g.now.year &&
                        g.currentDate.month == g.now.month &&
                        id + 1 == g.now.date) {
                        if (j == 0 || j == 6) {
                            $(td).addClass("l-box-dateeditor-holiday")
                        }
                        $(td).addClass("l-box-dateeditor-today");
                    }
                    else if (id < 0) {
                        showDay = prevMonthDayNum + showDay;
                        $(td).addClass("l-box-dateeditor-out")
                            .removeClass("l-box-dateeditor-selected");
                    }
                    else if (id > monthDayNum - 1) {
                        showDay = showDay - monthDayNum;
                        $(td).addClass("l-box-dateeditor-out")
                            .removeClass("l-box-dateeditor-selected");
                    }
                    else if (j == 0 || j == 6) {
                        $(td).addClass("l-box-dateeditor-holiday")
                            .removeClass("l-box-dateeditor-selected");
                    }
                    else {
                        td.className = "";
                    }

                    $(td).html(showDay);
                });
            });
        },
        updateSelectBoxPosition: function () {
            var g = this, p = this.options;
            if (p.absolute) {
                var contentHeight = $(document).height();
                if (Number(g.text.offset().top + 1 + g.text.outerHeight() + g.dateeditor.height()) > contentHeight
                    && contentHeight > Number(g.dateeditor.height() + 1)) {
                    g.dateeditor.css({ left: g.text.offset().left, top: g.text.offset().top - 1 - g.dateeditor.height() });
                } else {
                    g.dateeditor.css({ left: g.text.offset().left, top: g.text.offset().top + 1 + g.text.outerHeight() });
                }
            }
            else {
                if (g.text.offset().top + 4 > g.dateeditor.height() && g.text.offset().top + g.dateeditor.height() + textHeight + 4 - $(window).scrollTop() > $(window).height()) {
                    g.dateeditor.css("marginTop", -1 * (g.dateeditor.height() + textHeight + 5));
                    g.showOnTop = true;
                }
                else {
                    g.showOnTop = false;
                }
            }
        },
        toggleDateEditor: function (isHide) {
            var g = this, p = this.options;
            var managers = $.ligerui.find($.ligerui.controls.ComboBox);
            for (var i = 0, l = managers.length; i < l; i++) {
                var o = managers[i];
                if (o.id != g.id) {
                    if (o.selectBox.is(":visible") != null && o.selectBox.is(":visible")) {
                        o.selectBox.hide();
                    }
                }
            }
            managers = $.ligerui.find($.ligerui.controls.DateEditor);
            for (var i = 0, l = managers.length; i < l; i++) {
                var o = managers[i];
                if (o.id != g.id) {
                    if (o.dateeditor.is(":visible") != null && o.dateeditor.is(":visible")) {
                        o.dateeditor.hide();
                    }
                }
            }
            var textHeight = g.text.height();
            g.editorToggling = true;
            if (isHide) {
                g.dateeditor.hide('fast', function () {
                    g.editorToggling = false;
                });
            }
            else {
                g.updateSelectBoxPosition();
                g.dateeditor.slideDown('fast', function () {
                    g.editorToggling = false;
                });
            }
        },
        showDate: function () {
            var g = this, p = this.options;
            if (!this.currentDate) return;
            this.currentDate.hour = parseInt(g.toolbar.time.hour.html(), 10);
            this.currentDate.minute = parseInt(g.toolbar.time.minute.html(), 10);
            var dateStr = this.currentDate.year + '/' + this.currentDate.month + '/' + this.currentDate.date + ' ' + this.currentDate.hour + ':' + this.currentDate.minute;
            var myDate = new Date(dateStr);
            dateStr = g.getFormatDate(myDate);
            this.inputText.val(dateStr);
            this.onTextChange();
        },
        isDateTime: function (dateStr) {
            var g = this, p = this.options;
            var r = dateStr.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
            if (r == null) return false;
            var d = new Date(r[1], r[3] - 1, r[4]);
            if (d == "NaN") return false;
            return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4]);
        },
        isLongDateTime: function (dateStr) {
            var g = this, p = this.options;
            var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2})$/;
            var r = dateStr.match(reg);
            if (r == null) return false;
            var d = new Date(r[1], r[3] - 1, r[4], r[5], r[6]);
            if (d == "NaN") return false;
            return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4] && d.getHours() == r[5] && d.getMinutes() == r[6]);
        },
        getFormatDate: function (date) {
            var g = this, p = this.options;
            if (date == "NaN") return null;
            var format = p.format;
            var o = {
                "M+": date.getMonth() + 1,
                "d+": date.getDate(),
                "h+": date.getHours(),
                "m+": date.getMinutes(),
                "s+": date.getSeconds(),
                "q+": Math.floor((date.getMonth() + 3) / 3),
                "S": date.getMilliseconds()
            }
            if (/(y+)/.test(format)) {
                format = format.replace(RegExp.$1, (date.getFullYear() + "")
                    .substr(4 - RegExp.$1.length));
            }
            for (var k in o) {
                if (new RegExp("(" + k + ")").test(format)) {
                    format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k]
                        : ("00" + o[k]).substr(("" + o[k]).length));
                }
            }
            return format;
        },
        clear: function () {
            this.set('value', '');
            this.usedDate = null;
        },
        _setCancelable: function (value) {
            var g = this, p = this.options;
            if (!value && g.unselect) {
                g.unselect.remove();
                g.unselect = null;
            }
            if (!value && !g.unselect) return;
            g.unselect = $('<div class="l-trigger l-trigger-cancel"><div class="l-trigger-icon"></div></div>').hide();
            g.text.hover(function () {
                g.unselect.show();
            }, function () {
                g.unselect.hide();
            })
            if (!p.disabled && p.cancelable) {
                g.text.append(g.unselect);
            }
            g.unselect.hover(function () {
                this.className = "l-trigger-hover l-trigger-cancel";
            }, function () {
                this.className = "l-trigger l-trigger-cancel";
            }).click(function () {
                if (p.readonly) return;
                g.clear();
            });
        },
        _rever: function () {
            var g = this, p = this.options;
            if (!g.usedDate) {
                g.inputText.val("");
            } else {
                g.inputText.val(g.getFormatDate(g.usedDate));
            }
        },
        _getMatch: function (format) {
            var r = [-1, -1, -1, -1, -1, -1], groupIndex = 0, regStr = "^", str = format || this.options.format;
            while (true) {
                var tmp_r = str.match(/^yyyy|MM|dd|mm|hh|HH|ss|-|\/|:|\s/);
                if (tmp_r) {
                    var c = tmp_r[0].charAt(0);
                    var mathLength = tmp_r[0].length;
                    var index = 'yMdhms'.indexOf(c);
                    if (index != -1) {
                        r[index] = groupIndex + 1;
                        regStr += "(\\d{1," + mathLength + "})";
                    } else {
                        var st = c == ' ' ? '\\s' : c;
                        regStr += "(" + st + ")";
                    }
                    groupIndex++;
                    if (mathLength == str.length) {
                        regStr += "$";
                        break;
                    }
                    str = str.substring(mathLength);
                } else {
                    return null;
                }
            }
            return { reg: new RegExp(regStr), position: r };
        },
        _bulidDate: function (dateStr) {
            var g = this, p = this.options;
            var r = this._getMatch();
            if (!r) return null;
            var t = dateStr.match(r.reg);
            if (!t) return null;
            var tt = {
                y: r.position[0] == -1 ? 1900 : t[r.position[0]],
                M: r.position[1] == -1 ? 0 : parseInt(t[r.position[1]], 10) - 1,
                d: r.position[2] == -1 ? 1 : parseInt(t[r.position[2]], 10),
                h: r.position[3] == -1 ? 0 : parseInt(t[r.position[3]], 10),
                m: r.position[4] == -1 ? 0 : parseInt(t[r.position[4]], 10),
                s: r.position[5] == -1 ? 0 : parseInt(t[r.position[5]], 10)
            };
            if (tt.M < 0 || tt.M > 11 || tt.d < 0 || tt.d > 31) return null;
            var d = new Date(tt.y, tt.M, tt.d);
            if (p.showTime) {
                if (tt.m < 0 || tt.m > 59 || tt.h < 0 || tt.h > 23 || tt.s < 0 || tt.s > 59) return null;
                d.setHours(tt.h);
                d.setMinutes(tt.m);
                d.setSeconds(tt.s);
            }
            return d;
        },
        updateStyle: function () {
            this.onTextChange();
        },
        onTextChange: function () {
            var g = this, p = this.options;
            var val = g.inputText.val();
            if (!val) {
                g.selectedDate = null;
                return true;
            }
            var newDate = g._bulidDate(val);
            if (!newDate) {
                g._rever();
                return;
            }
            else {
                g.usedDate = newDate;
            }
            g.selectedDate = {
                year: g.usedDate.getFullYear(),
                month: g.usedDate.getMonth() + 1,
                day: g.usedDate.getDay(),
                date: g.usedDate.getDate(),
                hour: g.usedDate.getHours(),
                minute: g.usedDate.getMinutes()
            };
            g.currentDate = {
                year: g.usedDate.getFullYear(),
                month: g.usedDate.getMonth() + 1,
                day: g.usedDate.getDay(),
                date: g.usedDate.getDate(),
                hour: g.usedDate.getHours(),
                minute: g.usedDate.getMinutes()
            };
            var formatVal = g.getFormatDate(newDate);
            g.inputText.val(formatVal);
            g.trigger('changeDate', [formatVal]);
            if ($(g.dateeditor).is(":visible"))
                g.bulidContent();
        },
        _setHeight: function (value) {
            var g = this;
            if (value > 4) {
                g.text.css({ height: value });
                g.inputText.css({ height: value });
                g.textwrapper.css({ height: value });
            }
        },
        _setWidth: function (value) {
            var g = this;
            if (value > 20) {
                g.text.css({ width: value });
                g.inputText.css({ width: value - 20 });
                g.textwrapper.css({ width: value });
            }
        },
        _setValue: function (value) {
            var g = this;
            if (!value) g.inputText.val('');
            if (typeof value == "string") {
                if (/^\/Date/.test(value)) {
                    value = value.replace(/^\//, "new ").replace(/\/$/, "");
                    eval("value = " + value);
                }
                else {
                    g.inputText.val(value);
                }
            }
            if (typeof value == "object") {
                if (value instanceof Date) {
                    g.inputText.val(g.getFormatDate(value));
                    g.onTextChange();
                }
            }
        },
        _getValue: function () {
            return this.usedDate;
        },
        setEnabled: function () {
            var g = this, p = this.options;
            this.inputText.removeAttr("readonly");
            this.text.removeClass('l-text-disabled');
            p.disabled = false;
        },
        setDisabled: function () {
            var g = this, p = this.options;
            this.inputText.attr("readonly", "readonly");
            this.text.addClass('l-text-disabled');
            p.disabled = true;
        }
    });


})(jQuery);