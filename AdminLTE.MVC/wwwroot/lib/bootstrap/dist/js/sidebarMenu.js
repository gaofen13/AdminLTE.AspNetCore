(function ($) {
    $.fn.sidebarMenu = function (options) {
        options = $.extend({}, $.fn.sidebarMenu.defaults, options || {});
        var $menu_ul = $(this);
        var level = 0;
        //  target.addClass('nav');
        // target.addClass('nav-list');
        if (options.data) {
            init($menu_ul, options.data, level);
        }
        else {
            if (!options.url) return;
            $.getJSON(options.url, options.param, function (data) {

                init($menu_ul, data, level);
            });
        }

        function init($menu_ul, data, level) {
            $.each(data, function (i, item) {
                //如果标签是isHeader
                //如果不是header
                var li = $('<li class="treeview " data-level="' + level + '"></li>');

                //a标签
                var $a;
                if (level > 0) {
                    $a = $('<a style="padding-left:' + (level * 20) + 'px"></a>');
                } else {
                    $a = $('<a></a>');
                }

                //图标
                var $icon = $('<i></i>');
                $icon.addClass(item.icon);

                //标题
                var $title = $('<span class="title"></span>');
                $title.addClass('menu-text').text(item.text);

                $a.append($icon);
                $a.append($title);
                $a.addClass("nav-link");

                li.addClass("active");
                if (item.children && item.children.length > 0) {
                    var pullSpan = $('<span class="pull-right-container"></span>');
                    var pullIcon = $('<i class="fa fa-angle-left pull-right"></i>');
                    pullSpan.append(pullIcon);
                    $a.append(pullSpan);
                    li.append($a);

                    var menus = $('<ul></ul>');
                    menus.addClass('treeview-menu');
                    menus.css("display", "block");
                    menus.addClass("menu-open");

                    init(menus, item.children, level + 1);
                    li.append(menus);
                }
                else {

                    item.urlType = item.urlType ? item.urlType : 'relative';
                    var href = 'addTabs({id:\'' + item.id + '\',title: \'' + item.text + '\',close: true,url: \'' + item.url + '\',urlType: \'' + item.urlType + '\'});';
                    $a.attr('onclick', href);

                    $a.addClass("nav-link");
                    var badge = $("<span></span>");
                    // <span class="badge badge-success">1</span>
                    if (item.tip != null && item.tip > 0) {
                        badge.addClass("label").addClass("label-success").text(item.tip);
                    }
                    $a.append(badge);
                    li.append($a);
                }
                $menu_ul.append(li);
            });
        }

        //另外绑定菜单被点击事件,做其它动作
        $menu_ul.on("click", "li.treeview a", function () {
            var $a = $(this);

            if ($a.next().size() == 0) {//如果size>0,就认为它是可以展开的
                if ($(window).width() < $.AdminLTE.options.screenSizes.sm) {//小屏幕
                    //触发左边菜单栏按钮点击事件,关闭菜单栏
                    $($.AdminLTE.options.sidebarToggleSelector).click();
                }
            }
        });
    };

    $.fn.sidebarMenu.defaults = {
        url: null,
        param: null,
        data: null,
        isHeader: false
    };
})(jQuery);

//sidebar - menu组件封装
//在页面上面直接调用sidebar - menu的方法