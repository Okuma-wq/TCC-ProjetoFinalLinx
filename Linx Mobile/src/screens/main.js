import React, { Component } from 'react';
import { Image, StyleSheet, View } from 'react-native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';

import Home from './home';
import Perfil from './perfil';

const bottomTab = createBottomTabNavigator();

export default class Main extends Component{

  render(){
    return (
      <View style={styles.main}>
        <bottomTab.Navigator
        initialRouteName = 'Home'
        tabBarOptions = {{
            showLabel : false,
            showIcon : true,
            activeBackgroundColor : '#411E5A',
            inactiveBackgroundColor : '#5A3078',
            activeTintColor : '#FFF',
            inactiveTintColor : '#FFF',
            style : { height : 65 }
        }}
        screenOptions = { ({ route }) => ({
            tabBarIcon : () => {

            if (route.name === 'Home') {
                return(
                <Image 
                    source={require('../../assets/img/calendar.png')}
                    style={styles.tabBarIcon}
                />
                )
            }

            if (route.name === 'Perfil') {
                return(
                <Image 
                    source={require('../../assets/img/chamada.png')}
                    style={styles.tabBarIcon}
                />
                )
            }
            }
        }) }
        >
        <bottomTab.Screen name='Home' component={Home} />
        <bottomTab.Screen name='Perfil' component={Perfil} />
        </bottomTab.Navigator>
      </View>
    );
  }
}

const styles = StyleSheet.create({

  // conteúdo da main
  main: {
    flex: 1,
    backgroundColor: '#411E5A'
  },

  tabBarIcon : {
    width : 36,
    height : 36,
    //backgroundColor: '#FFF'
    tintColor: '#FFF'
  }
  
});
