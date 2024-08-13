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

Kibana Dev Tools sorgularını yazmanızı, test etmenizi ve çalıştırmanızı sağlar.

![image](https://github.com/user-attachments/assets/29375c79-5415-49c2-b8e8-5945062f980b)

<h3>Term Level Queries</h3>

Belgelerde belirli bir terimin tam olarak eşleşmesini arar. Bu sorgular, metin içeriğini değiştirmeden tam olarak aranan terimle eşleşen belgeleri bulur. Genellikle anahtar kelimeler ve belirli değerler ile çalışırken kullanılır. Genellikle **keyword** fieldlar ile Kullanılır. Genellikle küçük/büyük harf farklılıklarına veya boşluklara duyarsızdır.

**Term Query**: Elasticsearch'te belirli bir terimin tam olarak eşleştiği belgeleri bulmak için kullanılan bir sorgu türüdür. Bu sorgu, bir alanın belirli bir değere tam olarak eşleşip eşleşmediğini kontrol eder.

``POST /kibana_sample_data_ecommerce/_search
{
  "query": {
    "term": {
      "customer_id": 38
    }
  }
}``

![image](https://github.com/user-attachments/assets/6fce056f-2cd2-4e5a-9542-7f0728c62284)


**Prefix Query**: Bir alanın belirli bir ön ek ile başlayan terimlere sahip belgeleri bulmak için kullanılan bir sorgu türüdür. 

``POST /kibana_sample_data_ecommerce/_search
{
  "query": {
    "prefix": {
      "customer_full_name.keyword": {
        "value": "Son"
      }
    }
  }
}``

![image](https://github.com/user-attachments/assets/cd23e7d1-8f0c-4dcc-b434-d5eecfc463f6)


**Range Query**: Bir alanın belirli bir aralık içindeki değerleri içeren belgeleri bulmak için kullanılan bir sorgu türüdür. 

``POST /kibana_sample_data_ecommerce/_search
{
  "query": {
    "range": {
      "taxful_total_price": {
        "gte": 40,
        "lte": 50
      }
    }
  }
}``

![image](https://github.com/user-attachments/assets/48d3b7e5-ac8e-4a59-8fb5-551265d0f266)

![image](https://github.com/user-attachments/assets/c16c08c9-24c8-4ff8-8cba-d981eab496ee)


**Match All Query**: Tüm belgeleri eşleştiren bir sorgu türüdür. Bu sorgu, indeks içindeki tüm belgeleri döndürür.

``POST /kibana_sample_data_ecommerce/_search
{
  "query": {
    "match_all": {}
  }
}``

![image](https://github.com/user-attachments/assets/c9d275e8-9ce8-4ca1-959b-2013b09730dc)

**Pagination**:

``GET /my_index/_search
{
  "query": {
    "match_all": {}
  },
  "from": 10,
  "size": 10,
  "sort": [
    {
      "publish_date": {
        "order": "desc"
      }
    }
  ]
}
}``

![image](https://github.com/user-attachments/assets/760d567f-597f-409b-9ea3-d521c840cc59)

**Wildcard Query**:, Elasticsearch'te belirli bir alanın değerlerinin wildcard karakterleri kullanarak eşleşmesini sağlar. Bu sorgu türü, belirli bir desenle eşleşen terimleri aramak için kullanılır. "*" karakteri sıfır veya daha fazla karakteri temsil ederken, "?" karakteri tek bir karakteri temsil eder.

``POST /kibana_sample_data_ecommerce/_search
{
  "query": {
    "wildcard": {
      "email": {
        "value": "eddie*z"
      }
    }
  }
}``

![image](https://github.com/user-attachments/assets/df4d51cc-6734-4881-b449-0276f4d9cdd6)

**Fuzzy Query**: Kullanıcıların yazım hatalarını, varyasyonları veya benzer terimleri aramalarına olanak tanır.

 Fuzziness: Arama teriminin karakter bzında ne kadar "fuzzy" yani esnek olacağını belirler. Fuzziness.Auto kullanıldığında Elasticsearch, terimin uzunluğuna bağlı olarak uygun bir fuzziness değeri seçer. Ayrıca Fuzziness değeri olarak 1, 2, sayılar da belirtilebilir

``POST /kibana_sample_data_ecommerce/_search
{
  "query": {
    "fuzzy": {
      "customer_full_name.keyword": {
        "value": "Eddie Underworld",
        "fuzziness": "2"
      }
    }
  }
}``

![image](https://github.com/user-attachments/assets/81397d5d-d5b8-4faa-ac93-5830d281be38)

<h3>Full Text Queries</h3>

Br belge içindeki metin verisi üzerinde analiz ve eşleşme yapar. Bu sorgular, arama terimlerini daha esnek ve kapsamlı bir şekilde işlemek için metin analizi (örneğin, küçük harfe çevirme, durak kelimeleri çıkarma) tekniklerini kullanır. Text türündeki alanlarda tam metin araması yaparken kullanılır.

**Match Query**: 

``POST /kibana_sample_data_ecommerce/_search
{
  "query": {
    "match": {
      "customer_first_name": "Eddie"
    }
  }
}``

![image](https://github.com/user-attachments/assets/f67cfbec-d364-4388-b90a-8a9d89e079c0)

**Match Bool Prefix Query** Hem tam metin eşleşmeleri hem de prefix aramalarını bir arada yapılebilen querydir, default hali orlayarak getirir.

``POST /kibana_sample_data_ecommerce/_search
{
  "query": {
    "match_bool_prefix": {
      "products.product_name": {
        "query": "Boots - black Winter jacket - black"
      }
    }
  }
}``

![image](https://github.com/user-attachments/assets/98440e85-2ad2-405d-80cb-c89053ee18ee)

**Match Phrase Query**: Bir alan içinde tam olarak belirttiğiniz kelime öbeğini aramanızı sağlar.

``POST /kibana_sample_data_ecommerce/_search
{
  "query": {
    "match_phrase": {
      "customer_full_name": "Eddie" // Aramak istediğiniz kelime öbeği
    }
  }
}``

![image](https://github.com/user-attachments/assets/f97912fa-0ce1-4112-8b0a-2701798e6340)

**Compound Query**:, Birden fazla sorguyu bir araya getirerek daha karmaşık ve esnek arama işlemleri yapmanıza olanak tanır. Compound (bileşik) sorgular, birden fazla alt sorguyu birleştirerek, bu alt sorguların mantıksal bir kombinasyonunu oluşturur. Bu tür sorgular, farklı arama kriterlerini birleştirerek daha hassas ve anlamlı arama sonuçları elde etmeyi sağlar.

**must** içindeki şartı mutlaka sağlayan verileri getirir, skor değerine de katkı sağlar

**filter**  eşleşmek zorunda, skor değerine katkı sağlamaz

**should**  or gibi davranır, eşleşen dokümanda gözükebilir fakat zorunlu değildir, skora katkı sağlar

**must_not** olmasını istemediğimiz kayıtlar getiriyoruz

``POST /kibana_sample_data_ecommerce/_search
{
  "query": {
    "bool": {
      "must": [
        {
          "match": {
            "day_of_week": "Monday" // İlk sorgu terimi
          }
        }
      ],
      "should": [
        {
          "match": {
            "day_of_week": "Sunday" // İkinci sorgu terimi
          }
        }
      ],
      "must_not": [
        {
          "term": {
            "customer_gender": "FEMALE" // Hariç tutulacak terim
          }
        }
      ],
      "filter": [
        {
          "range": {
            "products.created_on": {
              "gte": "2010-01-01", // Tarih aralığı filtresi (başlangıç tarihi)
              "lt": "2025-01-01"   // Tarih aralığı filtresi (bitiş tarihi)
            }
          }
        }
      ]
    }
  }
}``

![image](https://github.com/user-attachments/assets/d3ac1d5f-98d5-4dec-bf0e-72bd80784ccc)


