#!/bin/groovy

node ('xamarin-macios') {
    stage ("Build") {
        def currentStage = "foo"
        def authorName = env.CHANGE_AUTHOR_DISPLAY_NAME
        def authorEmail = env.CHANGE_AUTHOR_EMAIL
        def slackMessage = "Pull Request #<${env.CHANGE_URL}|${env.CHANGE_ID}> failed to build."
        def title = "Internal jenkins failed in stage '${currentStage}'"
        def attachments = """
        [
            {
                \"author_name\": \"${authorName} (${authorEmail})\",
                \"title\": \"${title}\",
                \"title_link\": \"${env.RUN_DISPLAY_URL}\",
                \"color\": \"danger\",
                \"text\": \"some text\",
                \"fallback\": \"Build failed\"
            }
        ]
        """
        echo (attachments)
        slackSend (channel: "@rolf", color: "danger", message: slackMessage, attachments: attachments)
    }
} // node
