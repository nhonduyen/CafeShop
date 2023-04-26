import React, { Component } from 'react';
import { Item } from './Item';

export class MenuItems extends Component {

    render() {
        return (
            <div className="container">
                <div className="row" id="itemlist">
                    {
                        this.props.items.map(item => (
                            <Item menuItem={item} />
                        ))
                    }
                </div>
            </div>
        );
    }
}
