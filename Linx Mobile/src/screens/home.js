  import React, { Component } from 'react';
  import { FlatList, Image, StyleSheet, Text, TouchableOpacity, View } from 'react-native';
  import AsyncStorage from '@react-native-async-storage/async-storage';

  import api from '../services/api';
  import axios from 'axios';
  import jwtDecode from 'jwt-decode';
  import { Editar } from './editar';

  export default class Eventos extends Component {
    constructor(props) {
      super(props);
      this.state = {
        listaReunioes: [],
        idReuniao: '',
      
        Editar: '',
      }
    };

    Editar = (id) => {

      try {

        console.log(id)

        this.setState(() => localStorage.getItem("id",id))

        console.log(id)

        this.props.navigation.navigate('Editar');
      }

      catch (error) {

        console.log(error)

      }

    }

    listaReunioes = () => {
      console.log('Esta função traz todos os reunioes.')

      fetch('http://localhost:5000/api/Reuniao/Minhas', {
        headers: {
          'Authorization': 'Bearer ' + localStorage.getItem('userToken')
        }
      })

        .then(resposta => {
          if (resposta.status !== 200) {
            throw Error();
          }

          return resposta.json();
        })

        .then(resposta => this.setState({ listaReunioes: resposta }))

        .catch(erro => console.log(erro));
    };

    componentDidMount() {
      this.listaReunioes();
      this.setState({})

      
    };

    render() {
      return (
        <View style={styles.main}>

          <View style={styles.mainHeader}>
            <View style={styles.mainHeaderRow}>
              <Text style={styles.mainHeaderText}>{"Reuniões".toUpperCase()}</Text>
            </View>
            <View style={styles.mainHeaderLine} />
          </View>

          {/* Corpo - Body - Section */}
          <View style={styles.mainBody}>
            <FlatList
              contentContainerStyle={styles.mainBodyContent}
              data={this.state.listaReunioes}
              keyExtractor={item => item.nome}
              renderItem={this.renderItem}
            />
          </View>

        </View>
      );
    }

    renderItem = ({ item }) => (

      <View style={styles.flatItemRow}>
        <View style={styles.flatItemContainer}>
        <Text style={styles.flatItemInfo}>{item.titulo}</Text>
        <Text style={styles.flatItemInfo}>{item.nome}</Text>
          <Text style={styles.flatItemInfo}>{item.local}</Text>
          <Text style={styles.flatItemInfo}>{item.razaoSocial}</Text>
          <Text style={styles.flatItemInfo}>{Intl.DateTimeFormat("pt-BR", {
              year: 'numeric', month: 'numeric', day: 'numeric',
              hour: 'numeric', minute: 'numeric',
              hour12: false
              }).format(new Date(item.dataReuniao))}</Text>
        </View>

        <TouchableOpacity
          onPress={() => this.props.navigation.navigate('Editar', item)}
          style={styles.flatItemImg}
        >
          <View>
            <Image
              source={require('../../assets/img/lapis.png')}
              style={styles.flatItemImgIcon}
            />
          </View>
        </TouchableOpacity>
      </View>
    )
  }

  const styles = StyleSheet.create({

    // conteúdo da main
    main: {
      flex: 1,
      backgroundColor: '#F1F1F1',
      borderRadius: 50,

    },

    // cabeçalho
    mainHeader: {
      flex: 1,
      justifyContent: 'center',
      alignItems: 'center',
      backgroundColor: '#FFB200'
    },
    mainHeaderRow: {
      flexDirection: 'row'
    },
    // imagem do cabeçalho
    mainHeaderImg: {
      width: 22,
      height: 22,
      tintColor: '#ccc',
      marginRight: -5,
      marginTop: -12
    },
    // texto do cabeçalho
    mainHeaderText: {
      fontSize: 16,
      letterSpacing: 5,
      color: '#000000',
    },
    // linha de separação do cabeçalho
    mainHeaderLine: {
      width: 220,
      paddingTop: 10,
      borderBottomColor: '#000000',
      borderBottomWidth: 1
    },

    // conteúdo do body
    mainBody: {
      flex: 4,
      backgroundColor: '#FFB200'
    },
    // conteúdo da lista
    mainBodyContent: {
      paddingTop: 5,
      paddingRight: 40,
      paddingLeft: 40,
      marginTop: 5,
      marginBottom: 18,
      // backgroundColor: 'lightgreen'
    },


    flatItemContainer: {
      flex: 1,
      backgroundColor: '#411E5A',
      //shadowOpacity: 0.4,
      shadowRadius: 5,
      //border: '1px solid #fff',
      filter: 'drop-shadow(10px 10px 4px rgba(0, 0, 0, 0.25))',
      //borderRadius: 5,
      //borderWidth: '10px',
      //borderBottomColor: 'white',
    }, 

    flatItemTitle: {
      fontFamily: 'Roboto',
      fontSize: 20,
      fontWeight: "600",
      color: '#282f66',
      marginTop: 10,
    },

    flatItemTitle: {
      fontSize: 16,
      color: '#333',
    },

    flatItemInfo: {
      fontFamily: 'Roboto',
      fontSize: 16,
      color: '#FFF',
      lineHeight: 20,
      textAlign: 'justify',
      marginLeft: '10px'
    },

    flatItemImg: {
      justifyContent: 'center',
      
    },
    flatItemImgIcon: {
      width: 26,
      height: 26,
      tintColor: '#5A3078'
    },
    flatItemRow: {
      marginTop: 20,
    },
    

  });