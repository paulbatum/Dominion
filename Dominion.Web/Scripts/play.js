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
    submitCardSelection: function (event, activity) {
        var ids = $('#hand .selectedCard')
                            .get()
                            .map($.tmplItem)
                            .map(function (tmpl) { return tmpl.data.Id });

        if (activity.Type == "SelectUpToNumberOfCards" && ids.length > activity.Properties.NumberOfCardsToSelect) {
            showError('Error: Cannot select more than ' + activity.Properties.NumberOfCardsToSelect + ' cards.');
        }
        else {
            $.post('SelectCards', { ids: ids }, handleInteractionResponse);
        }
    },
    toggleCardSelection: function (event, activity) {
        if (activity.Properties.CardMustHaveName) {
            var selectionName = $.tmplItem(event.target).data.Name;
            if (activity.Properties.CardMustHaveName !== selectionName) {
                showError("Error: Must select only cards named '" + activity.Properties.CardMustHaveName + "'.");
                return;
            }
        }

        $(event.target).toggleClass('selectedCard');
    },
    makeChoice: function (choice) {
        $.post('MakeChoice', { choice: choice }, handleInteractionResponse);
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
            

            doChatComet();
            doGameComet();

            bindCommands();

            actions.chat("(SYSTEM) I'm here!");
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
            controller.PlayAreaClick = function (event) { };

            //This should be generalised to add/remove choice elements from the DOM
            $('#choiceDrawCards').hide();
            $('#choiceGainActions').hide();
            $('#choiceYes').hide();
            $('#choiceNo').hide();
            $('#doneChoice').hide();

            if (activity.Type == "DoBuys") {
                controller.BankClick = actions.buy;
            }

            if (activity.Type == "PlayActions") {
                controller.HandClick = actions.play;                
            }

            if (activity.Type == "SelectFromRevealed") {
                controller.PlayAreaClick = function (event) { actions.selectFixedNumberOfCards(event, activity); };
            }

            if (activity.Type == "SelectFixedNumberOfCards") {
                controller.HandClick = function (event) { actions.selectFixedNumberOfCards(event, activity); };
            }

            if (activity.Type == "MakeChoice") {
                for (var iOption in activity.Properties["AllowedOptions"]) {
                    var optionName = activity.Properties["AllowedOptions"][iOption];
                    var elementName = '#choice' + optionName;
                    $(elementName).show();
                }
            }

            if (activity.Type == "SelectUpToNumberOfCards") {
                controller.HandClick = function (event) { actions.toggleCardSelection(event, activity); };
                controller.DoneClick = function (event) { actions.submitCardSelection(event, activity); };
                $('#doneChoice').show();
            }

            if (activity.Type == "SelectPile") {
                controller.BankClick = function (event) { actions.selectPile(event, activity); };
            }            

        }

        function updateGameState(data) {

            updateSection('#bank', data.Bank, '#cardpileTemplate');
            updateSection('#hand', data.Hand, '#cardTemplate');
            updateSection('#status', data.Status, '#statusTemplate');            
            updateSection('#deck', data.Deck, '#deckTemplate');
            updateSection('#discards', data.Discards, '#discardpileTemplate');
            updateSection('#playArea', data.Revealed || data.InPlay, '#cardTemplate');

            $('#bank > .cardpile:gt(0)')                
                .css('margin-left','-15px');

            if (data.PendingActivity) {
                bindActivity(data.PendingActivity)
                renderPendingActivity(data.PendingActivity);
            }
            else {
                alert('wtf!');
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
                $('#prompt').addClass('waitingForOthers');
            else
                $('#prompt').removeClass('waitingForOthers');
        }

        function doChatComet() {
            $.ajax({
                url: 'chatloop',
                success: function (data) { $.ajax(this); updateChat(data); },
                timeout: 30000,
                error: function () { alert("Communication with the server has failed. Refresh the page."); },
                cache: false
            });
        }

        function doGameComet() {            
            $.ajax({
                url: 'gamestateloop',
                data: { mostRecentVersion: version },
                success: function (gameState) {
                    version = gameState.Version;
                    doGameComet();
                    updateGameState(gameState);
                },
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
            $('#playArea .card').live('click', function (event) { controller.PlayAreaClick(event); });
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

            $('#choiceDrawCards')
                    .click(function () { actions.makeChoice('DrawCards'); })
                    .button();

            $('#choiceGainActions')
                    .click(function () { actions.makeChoice('GainActions'); })
                    .button();

            $('#choiceYes')
                    .click(function () { actions.makeChoice('Yes'); })
                    .button();

            $('#choiceNo')
                    .click(function () { actions.makeChoice('No'); })
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
        

    