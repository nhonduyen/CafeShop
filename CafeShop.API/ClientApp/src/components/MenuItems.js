import React, { useContext } from 'react';
import { Item } from './Item';
import { ItemsContext } from './Home'

export function MenuItems() {

    const { itemList } = useContext(ItemsContext)

    return (
        <div className="container">
            <div className="row" id="itemlist">
                {
                    itemList.map(item => (
                        <Item menuItem={item} key={Math.random() * 10000000} />
                    ))
                }
            </div>
        </div>
    );
}
