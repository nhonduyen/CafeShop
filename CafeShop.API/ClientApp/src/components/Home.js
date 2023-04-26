import React, { Component } from 'react';
import { Header } from './Header';
import { SideMenu } from './SideMenu'
import { NavMenu } from './NavMenu';
import { MenuItems } from './MenuItems';

import './site.css'

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = {
      categories: [],
      selectedCategoryId: null,
      isShowSideMenu: false,
      itemList: []
    };

    this.generateItems = this.generateItems.bind(this);
    this.generateCategories = this.generateCategories.bind(this);
    this.updateIsShowSideMenu = this.updateIsShowSideMenu.bind(this);
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
    this.setState({ itemList: items })
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
        cateId: Math.floor((Math.random() * 15) + 1)
      };
      items.push(item);
    }
    this.setState({ categories: items })
  }
  updateIsShowSideMenu(isShow) {
    this.setState({isShowSideMenu: isShow})
  }

  updateSelectedCategoryId(id) {
    this.setState({ selectedCategoryId: id })
  }

  render() {
    return (
      <>
        <Header updateIsShowSideMenu={this.updateIsShowSideMenu} />
        <NavMenu categories={this.state.categories} selectedCategoryId={this.state.selectedCategoryId} />
        <SideMenu isShowSideMenu={this.state.isShowSideMenu} categories={this.state.categories} selectedCategoryId={this.state.selectedCategoryId} />
        <MenuItems items={this.state.itemList}/>
      </>
    );
  }
}
