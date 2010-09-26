var actions = {
    buy: function (event) {
        var data = $.tmplItem(event.target).data;
        if (data.CanBuy) {
            $.post('BuyCard', { id: data.Id }, handleInteractionResponse);
            $(event.target).effect('transfer', { to: '#discards .card' }, 500);
        }
        else {
            showError('Error: Cannot buy.');
        }
    },
    play: function (event) {
        var data = $.tmplItem(event.target).data;
        if (data.CanPlay) {
            $.post('PlayCard', { id: data.Id }, handleInteractionResponse);
            $(event.target).effect('transfer', { to: '.playAreaTransferTarget' }, 500);
        }
        else {
            showError('Error: Cannot play.');
        }
    },
    selectFixedNumberOfCards: function (event, activity) {
        var ids = [];

        if (activity.Properties.NumberOfCardsToSelect > 1) {
            $(event.target).toggleClass('selectedCard');

            ids = $('#hand .selectedCard')
                            .get()
                            .map($.tmplItem)
                            .map(function (tmpl) { return tmpl.data.Id });
        }
        else {
            ids.push($.tmplItem(event.target).data.Id);
        }

        if (ids.length == activity.Properties.NumberOfCardsToSelect) {
            $.post('SelectCards', { ids: ids }, handleInteractionResponse);
        }
    },
    submitCardSelection: function () {
        var ids = $('#hand .selectedCard')
                            .get()
                            .map($.tmplItem)
                            .map(function (tmpl) { return tmpl.data.Id });

        $.post('SelectCards', { ids: ids }, handleInteractionResponse);
    },
    toggleCardSelection: function (event, activity) {
        $(event.target).toggleClass('selectedCard');
    },
    makeYesNoChoice: function (choice) {
        $.post('MakeYesNoChoice', { choice: choice }, handleInteractionResponse);
    },
    selectPile: function (event, activity) {
        var data = $.tmplItem(event.target).data;
        $.post('SelectPile', { id: data.Id }, handleInteractionResponse);
    },
    chat: function (text) {
        if (text != "") {
            $.post('Chat', { message: text }, handleInteractionResponse);
        }
    }
};

        var controller = {};

        var version = 0;

        var middleLayout;

        $(document).ready(function () {
            jQuery.ajaxSettings.traditional = true;


            createLayout();
            setupChat();
            setupHover();
            bindDefaultClickEvents();

            loadGame();
            doComet('gamestateloop', updateGameState);
            doComet('chatloop', updateChat);

            bindCommands();
        });

        function setupChat() {
            $('#chat').hide();

            $(document).bind('keydown', 'return', function () {
                $('#chat').show();
                $('#chatBox').focus();
            });

            $('#chatBox').bind('keydown', 'return', function (event) {
                event.stopPropagation();
                actions.chat($('#chatBox').val());
                $('#chat').hide();
                $('#chatBox').val('');
            });
        }

        function bindActivity(activity) {
            controller.HandClick = function (event) { };
            controller.BankClick = function (event) { };
            controller.DoneClick = function (event) { };

            if (activity.Type == "SelectFixedNumberOfCards") {
                controller.HandClick = function (event) { actions.selectFixedNumberOfCards(event, activity); };
            }

            if (activity.Type == "MakeYesNoChoice") {
                $('#yesChoice').show();
                $('#noChoice').show();
            }
            else {
                $('#yesChoice').hide();
                $('#noChoice').hide();
            }

            if (activity.Type == "SelectAnyNumberOfCards") {
                controller.HandClick = function (event) { actions.toggleCardSelection(event, activity); };
                controller.DoneClick = actions.submitCardSelection;
                $('#doneChoice').show();
            }
            else {
                $('#doneChoice').hide();
            }

            if (activity.Type == "SelectPile") {
                controller.BankClick = function (event) { actions.selectPile(event, activity); };
            }

        }

        function loadGame() {            
            $.ajax({
                url: 'GameData',
                dataType: 'json',
                data: {},
                success: updateGameState,
                async: false
            });
        }

        function updateGameState(data) {

            if (version == data.Version)
                return;
            else
                version = data.Version;

            updateSection('#bank', data.Bank, '#cardpileTemplate');
            updateSection('#hand', data.Hand, '#cardTemplate');
            updateSection('#status', data.Status, '#statusTemplate');
            updateSection('#playArea', data.InPlay, '#cardTemplate');
            updateSection('#deck', data.Deck, '#deckTemplate');
            updateSection('#discards', data.Discards, '#discardpileTemplate');


            if (data.PendingActivity) {
                bindActivity(data.PendingActivity)
                renderPendingActivity(data.PendingActivity);
            }
            else {
                bindDefaultClickEvents();
                middleLayout.close('south');
            }

            $('#playArea')
                .append($('<div>').addClass('playAreaTransferTarget'));

            $('#log')
                .html(data.Log)                
                .animate({ scrollTop: $('#log').attr("scrollHeight") - $('#log').height() }, 1000);
        }

        function renderPendingActivity(pendingActivity) {
            middleLayout.open('south');
            $('#message').text(pendingActivity.Message);

            if (pendingActivity.Type == "WaitingForOtherPlayers")
                $('#alertBox').hide();
            else
                $('#alertBox').show();
        }

        function bindDefaultClickEvents() {
            controller.HandClick = actions.play;
            controller.BankClick = actions.buy;
        }

        function doComet(url, success) {
            $.ajax({
                url: url,
                success: function (data) { $.ajax(this); success(data); },
                timeout: 30000,
                error: function () { alert("Communication with the server has failed. Refresh the page."); },
                cache: false
            });
        }        

        function updateChat(data) {
            if (data.message != '') {
                $('#chatLog')                    
                    .append(data.message)
                    .append('<br/>')
                    .animate({ scrollTop: $('#chatLog').attr("scrollHeight") - $('#chatLog').height() }, 1000);
            }
        }

        function showError(message) {
            var errorDiv = $('<div>')
                .css('color', 'red')
                .text(message);

            $('#chatLog')
                .append(errorDiv)                    
                .animate({ scrollTop: $('#chatLog').attr("scrollHeight") - $('#chatLog').height() }, 1000);
        }

        function createLayout() {
            var defaults = {
                resizable: false,
                spacing_open: 0,
                autoResize: true
            };

            $('body').layout({
                defaults: defaults,
                north: {                    
                    size: '40%'
                },
                center: {                    
                    size: '30%'
                },
                south: {                    
                    size: '30%'
                }
            });

            $('#main').layout({
                defaults: defaults,
                east: {
                    size:'20%'
                },     
                center: {
                    size:'70%'
                }, 
                west: {
                    size:'10%'            
                }                         
            });

            middleLayout = $('#middle').layout({
                defaults: defaults,

                south: {
                    size: '30%' 
                },
                east: {
                    size: '30%'                    
                }
                
            });

            $('#bottom').layout({
                defaults: defaults,

                south: {
                    initClosed: true
                }
            });            
        }       

        function updateSection(sectionSelector, data, templateSelector) {
            $(sectionSelector)
                .html($(templateSelector).tmpl(data));
        }

        function bindCommands() {
            $('#hand .card').live('click', function (event) { controller.HandClick(event); });
            $('#bank .cardpile').live('click', function (event) { controller.BankClick(event); });

            $('#yesChoice')
                    .click(function () { actions.makeYesNoChoice(true); })
                    .button();

            $('#noChoice')
                    .click(function () { actions.makeYesNoChoice(false); })
                    .button();

            $('#doneChoice')
                    .click(function () { controller.DoneClick(); })
                    .button();

            $('form').ajaxForm(handleInteractionResponse);

            $('#doBuys').button();
            $('#endTurn').button();
        }

        function setupHover() {
            $('.cardpile img').thumbPopup({
                imgSmallFlag: "",
                imgLargeFlag: "",
                cursorTopOffset: -50,
                cursorLeftOffset: 15
            });
        }

        function handleInteractionResponse(response) {
            
        }
        

    