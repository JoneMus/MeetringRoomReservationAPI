### 1. Mitä tekoäly teki hyvin? 
Tekoäly loi pääosin projektin rakenteen hyvin ja projektin aloitus oli todella helppoa, kun rytmitti ohjeistuksen
omaan tekemiseen sopivaksi. 
### 2. Mitä tekoäly teki huonosti? 
Tekoäly ei huomannut projektin alustus vaiheessa luoda gitignore-tiedostoa.
Myös sovelluksen turvallisuuden kannalta tärkeät asiat olivat puutteellisia sekä pieniä logiikkavirheitä löytyi koodista. 
Vaikka koodi näytti päälisinpuolin hyvältä se sisälsi melko selkeitäkin virheitä.

### 3. Mitkä olivat tärkeimmät parannukset, jotka teit tekoälyn tuottamaan koodiin ja miksi? 
- Tekoälyn alkuperäisessä koodissa ei huomioitu "huoneita" eikä niiden olemassa oloa mitenkään ja käyttäjä pystyi tekemään varauksen huoneeseen mitä ei ole edes olemassa. Muutin sovelluksen logiikkaa siten, että varausten tekemisessä ja hakemisessa otettiin myös luodut huoneet huomioon.
- Alkuperäisen ohjeistuksen mukaan testikansio luotiin projektikansion sisään, mikä tuotti ongelmia testien ajamisessa. Järjestelin kansiorakenteen uudelleen siten, että testikansio ja projektikansio ovat samalla tasolla.
- Tein myös muutoksia virheenhallintaa ja varmistin, että huonevarausta tehdessä, palvelin hyväksyy vain objektit jotka sisältävät kaikki tarvittavat arvot.
