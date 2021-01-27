import React, { PureComponent } from 'react'
import { Segment } from 'semantic-ui-react'

export default class MediaList extends PureComponent {
    render() {
        return (
            <div className="media-list">
                <Segment>
                    Info about track 1
                </Segment>

                <Segment>
                    Info about track 2
                </Segment>

                <Segment>
                    Info about track 3
                </Segment>                            
            </div>
        )
    }
}
