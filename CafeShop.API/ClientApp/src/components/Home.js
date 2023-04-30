import React, { Component } from 'react';
import { createContext } from "react";
import { Header } from './Header';
import { SideMenu } from './SideMenu'
import { NavMenu } from './NavMenu';
import { MenuItems } from './MenuItems';
import { ItemDetails } from './ItemDetails';

import './site.css'

export const CategoriesContext = createContext()
export const ItemsContext = createContext()
export class Home extends Component {
  static displayName = Home.name;


  constructor(props) {
    super(props);
    this.state = {
      categories: {
        categoriesList: [],
        selectedCategory: {},
        isShowSideMenu: false
      },
      items: {
        itemList: [],
        selectedItem: null,
        isShowItemDetails: false
      }
    }

    this.generateItems = this.generateItems.bind(this);
    this.generateCategories = this.generateCategories.bind(this);
    this.updateIsShowSideMenu = this.updateIsShowSideMenu.bind(this);
    this.updateIsShowItemDetails = this.updateIsShowItemDetails.bind(this);
    this.updateSelectedCategoryId = this.updateSelectedCategoryId.bind(this);
  }

  componentDidMount() {
    this.generateItems()
    this.generateCategories()
  }

  generateItems() {
    let items = []
    for (let i = 65; i < 90; i++) {
      let price = Math.floor((Math.random() * 200000) + 1000);

      let item = {
        title: `Món ăn ${String.fromCharCode(i)}`,
        desc: `Mô tả món ăn ${String.fromCharCode(i)}`,
        price: price,
        url: `./img/BK4PV${String.fromCharCode(i)}.jpg`,
        cateId: Math.floor((Math.random() * 15) + 1),
        id: i,
        priceWithComma: this.numberWithCommas(price)
      };
      items.push(item);
    }
    this.setState({ items: { itemList: items } })
  }

  numberWithCommas(x) {
    if (isNaN(x))
      return -1;
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")
  }

  generateCategories() {
    let items = []
    for (let i = 65; i < 90; i++) {
      let item = {
        name: `Menu ${String.fromCharCode(i)}`,
        cateId: i
      };
      items.push(item);
    }
    this.setState(({ categories }) => ({ categories: {
      ...categories,
      categoriesList: items,
    }}))
  }
  updateIsShowSideMenu(isShow) {
    this.setState({ isShowSideMenu: isShow })
  }

  updateSelectedCategoryId(id) {
    this.setState({ selectedCategoryId: id })
  }

  updateIsShowItemDetails(isShow) {
    this.setState({ isShowItemDetails: isShow })
  }

  render() {
    return (
      <>
        <Header updateIsShowSideMenu={this.updateIsShowSideMenu} />
        <CategoriesContext.Provider value={this.state.categories}>
          <NavMenu selectedCategoryId={this.state.selectedCategoryId} />
          <SideMenu isShowSideMenu={this.state.isShowSideMenu} categories={this.state.categories} selectedCategoryId={this.state.selectedCategoryId} />
        </CategoriesContext.Provider>
        <ItemsContext.Provider value={this.state.items}>
          <MenuItems />
          <ItemDetails />
        </ItemsContext.Provider>
      </>
    );
  }
}
