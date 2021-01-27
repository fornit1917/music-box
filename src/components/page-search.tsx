import React, { PureComponent } from 'react'
import { Input } from 'semantic-ui-react'
import MediaList from './media-list/media-list'

export default class PageSearch extends PureComponent {
    render() {
        return (
            <div>
                <Input
                    size="big"
                    className="search-input"
                    placeholder="Link or Query"
                />
                
                <div className="search-results">
                    <MediaList/>
                </div>
            </div>
        )
    }
}
