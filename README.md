Kurulum için Docker Desktop üzerinde ElasticSearch ve Kibana containerları gereklidir.

![image](https://github.com/user-attachments/assets/06d77757-e53b-4b2d-81a9-1e45fd190287)
![image](https://github.com/user-attachments/assets/cbdd76f1-6925-4781-af08-b22745384413)

<br>

Aşağıdaki sekmeden Sample data eklenebilir. Bu solutionda "kibana_sample_data_ecommerce" kullamnılmıştir.

![image](https://github.com/user-attachments/assets/072ebcc7-7870-403d-bd4d-15437aa04d62)

.Net Api üzerinden sorgulama yapmak için Elastic.Clients.ElasticSearch kullanılmıştır.

![image](https://github.com/user-attachments/assets/c36815b1-3c9f-48f1-9a12-539693b7423b)

<br><br>

<h2>Bazı Temel Kavramlar</h2>

**Document:** Indekslenen ve sorgulanan temel veridir ve JSON formatında saklanır. İçerisinde fieldlar bulunur.

**Index**: verrilerin saklandığı, organize edildiği ve arandığı temel veri yapısıdır. Bir indeks, ilişkili belgeler koleksiyonunu içerir ve belirli bir mappinge sahiptir. Verileri efektif bir şekilde sorgulamak ve analiz etmek için kullanılır.

**Field**: Her belge, birden fazla alana sahip olabilir. Her alan, bir key-value çiftinden oluşur

**Mapping**: Bir belgenin hangi alanları içereceğini, bu alanların veri türlerini ve nasıl indeksleneceğini belirleyen bir yapılandırmadır.

![image](https://github.com/user-attachments/assets/2c3626a3-5b14-472b-8f5e-b648780fa5dc)

**Önemli Mapping Type'lar**

**text**: Tam metin aramaları için kullanılır. Metin analizi yapılır, yani metin parçalara ayrılır ve çeşitli dönüşümler uygulanır, büyük metin alanları için idealdir.

**keyword**: Anahtar kelime aramaları için kullanılır. Analiz yapılmaz, tam eşleşme gerektirir. Örneğin, etiketler, kategori isimleri gibi kısa ve değişmeyen metinler için uygundur.


Örnek bir mapping ve açıklaması aşağıda görülebilir;

      "customer_first_name": {
        "type": "text",
        "fields": {
          "keyword": {
            "type": "keyword",
            "ignore_above": 256
          }
        }
      },

"type": "text" ile customer_first_name alanının metin olarak indeksleneceği anlamına gelmektedir. Tam metin aramaları için kullanılır ve metin analizi yapılır.
"fields" ile Elasticsearch'te bir alanın farklı analiz türleriyle indekslenmesine izin veren bir verilir. Bu, aynı veriyi farklı amaçlar için kullanılabilir hale getirir.
"type": "keyword" ile customer_first_name alanının, "text" tipine ek olarak bir "keyword" alt alanı olarak da indeksleneceği anlamına gelir. Bu alt alan, tam eşleşme aramaları için kullanılır. keyword türü, analiz edilmez ve metnin tamamını olduğu gibi saklar.

**Text Analyzing**: Bir "text" alanının indekslenmeden önce belirli bir dizi işlemden geçirilmesi sürecidir. Bu süreç, metni daha etkili ve verimli bir şekilde aramak için Tokenization ve Normalization dönüştürmeyi içerir.

- **Tokenization**: Text boşluklar ve noktalama işaretleri gibi ayırıcılar kullanılarak daha küçük parçalara, yani tokenlara ayrılır. Örneğin, "Merhaba Dünya!" metni "Merhaba" ve "Dünya" şeklinde iki tokena ayrılabilir.

- **Normalization**: Text analizi sürecinde verilerin tutarlılığını ve karşılaştırılabilirliğini artırmak için uygulanan bir dizi işlemi ifade eder. Büyük/küçük harf duyarlılığını ortadan kaldırma, aynı köke sahip kelimeleri eşleme, eş anlamlı kelimeleri eşleme gibi aşamaları vardır.

  
**Relevancy Score**: Bir belgenin belirli bir arama sorgusuna ne kadar uygun olduğunu belirlemek için kullanılan bir ölçüttür. Arama sonuçlarının sıralanmasında kullanılır ve daha yüksek puana sahip belgeler, sorguyla daha iyi eşleşen belgeler olarak kabul edilir.

<br><br>

<h2>Implementasyon</h2>

<h3>Term Level Queries</h3>

Belgelerde belirli bir terimin tam olarak eşleşmesini arar. Bu sorgular, metin içeriğini değiştirmeden tam olarak aranan terimle eşleşen belgeleri bulur. Genellikle anahtar kelimeler ve belirli değerler ile çalışırken kullanılır. Genellikle **keyword** fieldlar ile Kullanılır. Genellikle küçük/büyük harf farklılıklarına veya boşluklara duyarsızdır.

**Term Query**: Elasticsearch'te belirli bir terimin tam olarak eşleştiği belgeleri bulmak için kullanılan bir sorgu türüdür. Bu sorgu, bir alanın belirli bir değere tam olarak eşleşip eşleşmediğini kontrol eder. Genellikle anahtar kelime (keyword) ve diğer tam değerli alanlarda kullanılır.

``POST /kibana_sample_data_ecommerce/_search
{
  "query": {
    "term": {
      "customer_id": 38
    }
  }
}``


